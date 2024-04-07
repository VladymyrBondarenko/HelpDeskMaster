using FluentAssertions;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Configuration;
using HelpDeskMaster.WebApi.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using HelpDeskMaster.WebApi.Contracts.User.Responses;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace HelpDeskMaster.E2ETests.EndpointsTests.Authentication
{
    public class KeycloakAuthenticationTests : IClassFixture<HdmServerApplicationFactory>
    {
        private readonly HdmServerApplicationFactory _factory;
        private readonly KeycloakAuthenticationService _keycloakAuthService;

        public KeycloakAuthenticationTests(HdmServerApplicationFactory factory)
        {
            _factory = factory;

            var authOptions = _factory.Services.GetRequiredService<KeycloakAuthenticationOptions>();
            _keycloakAuthService = new KeycloakAuthenticationService(authOptions.AuthServerUrl);
        }

        [Fact]
        public async Task ShouldReturnAccessToken()
        {
            // check authentication into keycloak
            var keycloakAuthResponse = await _keycloakAuthService
                .Invoking(x => x.AuthenticateToKeycloak())
                .Should().NotThrowAsync();

            keycloakAuthResponse
                .Subject.Should().NotBeNull();
            keycloakAuthResponse
                .Subject.AccessToken.Should().NotBeNullOrWhiteSpace();
            keycloakAuthResponse
                .Subject.RefreshToken.Should().NotBeNullOrWhiteSpace();

            // check that authenticated user is saved into users table
            const string login = "hdm-client-user@mail.com";

            using var httpClient = _factory.CreateClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "bearer", keycloakAuthResponse.Subject.AccessToken);

            using var response = await httpClient.GetAsync($"api/users/{login}");
            response.Invoking(x => x.EnsureSuccessStatusCode()).Should().NotThrow();

            var createdUser = await response.Content.ReadFromJsonAsync<ResponseBody<GetUserByLoginResponse>>();

            createdUser.Should().NotBeNull()
                    .And.Subject.As<ResponseBody<GetUserByLoginResponse>>()
                .Data.Should().NotBeNull()
                    .And.Subject.As<GetUserByLoginResponse>()
                .Login.Should().Be(login);
        }
    }
}
