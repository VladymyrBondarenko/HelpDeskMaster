using FluentAssertions;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using System.Net.Http.Json;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkCategoryEndpointTests : IClassFixture<HDMServerApplicationFactory>
    {
        private readonly HDMServerApplicationFactory _factory;

        public WorkCategoryEndpointTests(HDMServerApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnListOfWorkCategories()
        {
            using var httpClient = _factory.CreateClient();
            using var response = await httpClient.GetAsync("api/workCategories");

            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkCategoriesResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkCategoriesResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkCategoriesResponse>()
                .WorkCategories.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldCreateNewWorkCategory()
        {
            var request = new CreateWorkCategoryRequest("Category");

            using var httpClient = _factory.CreateClient();
            using var response = await httpClient.PostAsJsonAsync("api/workCategories",
                request, CancellationToken.None);

            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkCategoryResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateWorkCategoryResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateWorkCategoryResponse>()
                .Title.Should().Be(request.Title);
            reponseBody!.Data.Id.Should().NotBeEmpty();

            using var getResponse = await httpClient.GetAsync("api/workCategories");
            var categoriesData = await getResponse.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkCategoriesResponse>>();

            categoriesData.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkCategoriesResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkCategoriesResponse>()
                .WorkCategories.Should().OnlyContain(x => x.Title == request.Title);
        }

        [Fact]
        public async Task ShouldDeleteWorkCategory()
        {
            var request = new CreateWorkCategoryRequest("Category");

            using var httpClient = _factory.CreateClient();
            using var createResponse = await httpClient.PostAsJsonAsync("api/workCategories",
                request, CancellationToken.None);

            createResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var createReponseBody = await createResponse.Content.ReadFromJsonAsync<ResponseBody<CreateWorkCategoryResponse>>();
            createReponseBody!.Data.Id.Should().NotBeEmpty();

            using var deleteResponse = await httpClient.DeleteAsync($"api/workCategories/{createReponseBody!.Data.Id}");

            deleteResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            using var getResponse = await httpClient.GetAsync("api/workCategories");
            var categoriesData = await getResponse.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkCategoriesResponse>>();

            categoriesData.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkCategoriesResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkCategoriesResponse>()
                .WorkCategories.Should().BeEmpty();
        }
    }
}
