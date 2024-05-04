using FluentAssertions;
using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Persistence.Data;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkCategoryEndpointTests : HdmEndpointTestBase
    {
        private readonly HdmServerApplicationFactory _factory;

        public WorkCategoryEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnListOfWorkCategories()
        {
            await AuthenticateAsync();

            using var response = await HttpClient.GetAsync("api/workCategories");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkCategoriesResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkCategoriesResponse>>()
                .Data.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldCreateNewWorkCategory()
        {
            await AuthenticateAsync();

            var request = new CreateWorkCategoryRequest("Category");

            using var response = await HttpClient.PostAsJsonAsync("api/workCategories",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkCategoryResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateWorkCategoryResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateWorkCategoryResponse>()
                .Title.Should().Be(request.Title);
            reponseBody!.Data.Id.Should().NotBeEmpty();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var workCategoryInDb = await db.WorkCategories
                .SingleOrDefaultAsync(x => x.Id == reponseBody.Data.Id);

            workCategoryInDb.Should().NotBeNull()
                    .And.Subject.As<WorkCategory>()
                .Title.Should().Be(request.Title);
        }

        [Fact]
        public async Task ShouldDeleteWorkCategory()
        {
            await AuthenticateAsync();

            await using var scope = _factory.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var date = new DateTimeOffset(
                2024, 4, 28,
                12, 34, 5,
                new TimeSpan());

            var catetory = new WorkCategory(
                new Guid("a95ba4e8-be61-4bd9-8980-530a8df53c0b"), 
                "Work Category 1", 
                date);
            await db.WorkCategories.AddAsync(catetory);
            await db.SaveChangesAsync();

            using var deleteResponse = await HttpClient.DeleteAsync($"api/workCategories/{catetory.Id}");
            deleteResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            (await db.WorkCategories.AnyAsync(x => x.Id == catetory.Id))
                .Should().BeFalse();
        }
    }
}
