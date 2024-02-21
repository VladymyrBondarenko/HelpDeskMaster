using FluentAssertions;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using HelpDeskMaster.WebApi.Contracts;
using System.Net.Http.Json;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkDirectionEndpointTests : IClassFixture<HDMServerApplicationFactory>
    {
        private readonly HDMServerApplicationFactory _factory;

        public WorkDirectionEndpointTests(HDMServerApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ShouldReturnListOfWorkDirections()
        {
            using var httpClient = _factory.CreateClient();
            using var response = await httpClient.GetAsync("api/workDirections");

            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkDirectionsResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkDirectionsResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkDirectionsResponse>()
                .WorkDirections.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldCreateNewWorkDirection()
        {
            var request = new CreateWorkDirectionRequest("Direction");

            using var httpClient = _factory.CreateClient();
            using var response = await httpClient.PostAsJsonAsync("api/workDirections",
                request, CancellationToken.None);

            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var reponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkDirectionResponse>>();

            reponseBody.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<CreateWorkDirectionResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<CreateWorkDirectionResponse>()
                .Title.Should().Be(request.Title);
            reponseBody!.Data.Id.Should().NotBeEmpty();

            using var getResponse = await httpClient.GetAsync("api/workDirections");
            var directionsData = await getResponse.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkDirectionsResponse>>();

            directionsData.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkDirectionsResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkDirectionsResponse>()
                .WorkDirections.Should().OnlyContain(x => x.Title == request.Title);
        }

        [Fact]
        public async Task ShouldDeleteWorkDirection()
        {
            var request = new CreateWorkDirectionRequest("Direction");

            using var httpClient = _factory.CreateClient();
            using var response = await httpClient.PostAsJsonAsync("api/workDirections",
                request, CancellationToken.None);

            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var createReponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkDirectionResponse>>();
            createReponseBody!.Data.Id.Should().NotBeEmpty();

            using var deleteResponse = await httpClient.DeleteAsync($"api/workDirections/{createReponseBody!.Data.Id}");

            deleteResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            using var getResponse = await httpClient.GetAsync("api/workDirections");
            var directionsData = await getResponse.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkDirectionsResponse>>();

            directionsData.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkDirectionsResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkDirectionsResponse>()
                .WorkDirections.Should().BeEmpty();
        }
    }
}
