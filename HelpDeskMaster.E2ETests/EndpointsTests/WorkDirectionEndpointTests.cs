using FluentAssertions;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using HelpDeskMaster.WebApi.Contracts;
using System.Net.Http.Json;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HelpDeskMaster.Domain.Entities.WorkDirections;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkDirectionEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public WorkDirectionEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnListOfWorkDirections()
        {
            await AuthenticateAsync();

            using var response = await HttpClient.GetAsync("api/workDirections");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkDirectionsResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkDirectionsResponse>>()
                .Data.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldCreateNewWorkDirection()
        {
            await AuthenticateAsync();

            var request = new CreateWorkDirectionRequest("Direction");

            using var response = await HttpClient.PostAsJsonAsync("api/workDirections",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkDirectionResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateWorkDirectionResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateWorkDirectionResponse>()
                .Title.Should().Be(request.Title);
            reponseBody!.Data.Id.Should().NotBeEmpty();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var workDirectionInDb = await db.WorkDirections
                .SingleOrDefaultAsync(x => x.Id == reponseBody.Data.Id);

            workDirectionInDb.Should().NotBeNull()
                    .And.Subject.As<WorkDirection>()
                .Title.Should().Be(request.Title);
        }

        [Fact]
        public async Task ShouldDeleteWorkDirection()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var request = new CreateWorkDirectionRequest("Direction");

            var date = new DateTimeOffset(
                2024, 4, 28,
                12, 34, 5,
                new TimeSpan());

            var workDirection = new WorkDirection(
                new Guid("abb82714-3aa2-4d76-8e1d-b9a879443975"),
                "Work direction 1",
                date);
            await db.WorkDirections.AddAsync(workDirection);
            await db.SaveChangesAsync();

            using var deleteResponse = await HttpClient.DeleteAsync($"api/workDirections/{workDirection.Id}");
            deleteResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            (await db.WorkDirections.AnyAsync(x => x.Id == workDirection.Id))
                .Should().BeFalse();
        }
    }
}
