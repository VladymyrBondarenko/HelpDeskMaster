using FluentAssertions;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Responses;
using HelpDeskMaster.WebApi.Contracts;
using System.Net.Http.Json;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.WorkRequest.Requests;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class WorkDirectionEndpointTests : HdmEndpointTestBase
    {
        public WorkDirectionEndpointTests(HdmServerApplicationFactory factory) : base(factory)
        {
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

            using var getResponse = await HttpClient.GetAsync("api/workDirections");
            getResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

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
            await AuthenticateAsync();

            var request = new CreateWorkDirectionRequest("Direction");

            using var response = await HttpClient.PostAsJsonAsync("api/workDirections",
                request, CancellationToken.None);
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var createReponseBody = await response.Content.ReadFromJsonAsync<ResponseBody<CreateWorkDirectionResponse>>();
            createReponseBody!.Data.Id.Should().NotBeEmpty();

            using var deleteResponse = await HttpClient.DeleteAsync($"api/workDirections/{createReponseBody!.Data.Id}");
            deleteResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            using var getResponse = await HttpClient.GetAsync("api/workDirections");
            getResponse.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var directionsData = await getResponse.Content.ReadFromJsonAsync<ResponseBody<GetAllWorkDirectionsResponse>>();

            directionsData.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetAllWorkDirectionsResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetAllWorkDirectionsResponse>()
                .WorkDirections.Should().BeEmpty();
        }
    }
}
