using FluentAssertions;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.WebApi.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Domain.Entities.WorkRequests;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkRequestEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public WorkRequestEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnWorkRequestById()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // create user
            var user = User.Create(
                new Guid("ab11129a-7265-471f-b2c1-8f8c0f3bbd84"),
                new Login("44c22d86-e461-4c0c-a08f-5e5dc493c5bc@gmail.com"),
                "4bcd9ad8-ea56-4575-9804-88c7780e7d03");
            await db.Users.AddAsync(user);

            // create work category
            var workCategory = new WorkCategory(
                new Guid("c80465a0-e78c-4212-a6b2-ce1deacac5e5"),
                "Work Category 1",
                new DateTimeOffset(
                    2022, 1, 1,
                    20, 33, 5, new TimeSpan()));
            await db.WorkCategories.AddAsync(workCategory);

            // create work direction
            var workDirection = new WorkDirection(
               new Guid("51195e1c-722c-488b-9d71-73582ff00ed2"),
               "Work direction 1",
               new DateTimeOffset(
                    2023, 1, 1,
                    20, 33, 5, new TimeSpan()));
            await db.WorkDirections.AddAsync(workDirection);

            // create work request
            var failureDate = new DateTimeOffset(
                2024, 5, 10,
                20, 33, 5,
                new TimeSpan());

            var workRequest = WorkRequest.Create(
                new Guid("a96bb36a-6708-40d4-a83c-60e9dfb1019a"),
                new DateTimeOffset(
                    2023, 1, 1,
                    20, 33, 5, new TimeSpan()),
                user.Id,
                workDirection.Id,
                workCategory.Id,
                failureDate,
                failureDate,
                "Executer's hardware breakdown");
            await db.WorkRequests.AddAsync(workRequest);

            // save simulated data in db
            await db.SaveChangesAsync();

            // call for work request get by id endpoint
            using var response = await HttpClient.GetAsync($"api/workRequests/{workRequest.Id}", 
                CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetWorkRequestResponse>>();

            // validate response body
            reponseBody.Should().NotBeNull()
                .And.Subject.As<ResponseBody<GetWorkRequestResponse>>()
                .Data.Should().NotBeNull();

            reponseBody!.Data!.Id.Should().Be(workRequest.Id);
            reponseBody!.Data!.AuthorId.Should().Be(workRequest.AuthorId);
            reponseBody!.Data!.WorkCategory.Should().NotBeNull();
            reponseBody!.Data!.WorkCategory.Id.Should().Be(workCategory.Id);
            reponseBody!.Data!.WorkDirection.Should().NotBeNull();
            reponseBody!.Data!.WorkDirection.Id.Should().Be(workDirection.Id);
            reponseBody!.Data!.FailureRevealedDate.Should().Be(workRequest.FailureRevealedDate);
            reponseBody!.Data!.DesiredExecutionDate.Should().Be(workRequest.DesiredExecutionDate);
            reponseBody!.Data!.Content.Should().Be(workRequest.Content);
        }

        [Fact]
        public async Task ShouldCreateNewWorkRequest()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // create user
            var user = User.Create(
                new Guid("9d3d5796-77c6-432e-bcaf-dbc6912ae4f1"),
                new Login("72df06b0-773f-4fa5-b1bc-ae99da7fbbd9@gmail.com"),
                "4cc97ba8-e662-421d-82a7-3f7b87f177c2");
            await db.Users.AddAsync(user);

            // create work category
            var workCategory = new WorkCategory(
                new Guid("cb2b03c6-8584-4671-a928-6ec4236d326b"),
                "Work Category 1",
                new DateTimeOffset(
                    2022, 1, 1,
                    20, 33, 5, new TimeSpan()));
            await db.WorkCategories.AddAsync(workCategory);

            // create work direction
            var workDirection = new WorkDirection(
               new Guid("57ad33c3-b387-4c56-8fd8-216a6da527ac"),
               "Work direction 1",
               new DateTimeOffset(
                    2023, 1, 1,
                    20, 33, 5, new TimeSpan()));
            await db.WorkDirections.AddAsync(workDirection);

            // save simulated data in db
            await db.SaveChangesAsync();

            var failureDate = new DateTimeOffset(
                2024, 5, 10,
                20, 33, 5,
                new TimeSpan());

            var request = new CreateWorkRequestRequest(
                user.Id,
                workDirection.Id,
                workCategory.Id,
                failureDate,
                failureDate,
                "Work request creation testing");

            // call for work request creation endpoint
            using var response = await HttpClient.PostAsJsonAsync("api/workRequests", 
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkRequestResponse>>();
            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateWorkRequestResponse>>()
                .Data.Should().NotBeNull();

            reponseBody!.Data!.Id.Should().NotBeEmpty();
            reponseBody!.Data!.AuthorId.Should().Be(user.Id);
            reponseBody!.Data!.WorkDirectionId.Should().Be(request.WorkDirectionId);
            reponseBody!.Data!.WorkCategoryId.Should().Be(request.WorkCategoryId);
            reponseBody!.Data!.FailureRevealedDate.Should().Be(request.FailureRevealedDate);
            reponseBody!.Data!.DesiredExecutionDate.Should().Be(request.DesiredExecutionDate);
            reponseBody!.Data!.Content.Should().Be(request.Content);

            // assert work request in db
            var workRequestInDb = await db.WorkRequests
                .SingleOrDefaultAsync(x => x.Id == reponseBody.Data.Id);
            workRequestInDb.Should().NotBeNull();
        }
    }
}
