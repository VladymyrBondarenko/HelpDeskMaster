using FluentAssertions;
using HelpDeskMaster.WebApi.Contracts;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using System.Net.Http.Json;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkCategoryEndpointTests : HdmEndpointTestBase
    {
        public WorkCategoryEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
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

            using var getResponse = await HttpClient.GetAsync("api/workCategories");
            getResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

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
            await AuthenticateAsync();

            var request = new CreateWorkCategoryRequest("Category");

            using var createResponse = await HttpClient.PostAsJsonAsync("api/workCategories",
                request, CancellationToken.None);
            createResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var createReponseBody = await createResponse.Content.ReadFromJsonAsync<ResponseBody<CreateWorkCategoryResponse>>();
            createReponseBody!.Data.Id.Should().NotBeEmpty();

            using var deleteResponse = await HttpClient.DeleteAsync($"api/workCategories/{createReponseBody!.Data.Id}");
            deleteResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            using var getResponse = await HttpClient.GetAsync("api/workCategories");
            getResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var categoriesData = await getResponse.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkCategoriesResponse>>();

            categoriesData.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkCategoriesResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkCategoriesResponse>()
                .WorkCategories.Should().BeEmpty();
        }
    }
}
