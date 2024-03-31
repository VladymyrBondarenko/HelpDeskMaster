using System.Net.Http.Headers;
using Xunit;
using HelpDeskMaster.E2ETests.EndpointsTests.Authentication;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class HdmEndpointTestBase : IClassFixture<HdmServerApplicationFactory>, IDisposable
    {
        private readonly KeycloakAuthenticationService _keycloakAuthService;
        protected readonly HttpClient HttpClient;

        public HdmEndpointTestBase(HdmServerApplicationFactory factory)
        {
            var authOptions = factory.Services.GetRequiredService<KeycloakAuthenticationOptions>();
            _keycloakAuthService = new KeycloakAuthenticationService(authOptions.AuthServerUrl);

            HttpClient = factory.CreateClient();
        }

        public async Task AuthenticateAsync()
        {
            var keycloakAuthResponse = await _keycloakAuthService
                .Invoking(x => x.AuthenticateToKeycloak())
                .Should().NotThrowAsync();

            keycloakAuthResponse
                .Subject.Should().NotBeNull();
            keycloakAuthResponse
                .Subject.AccessToken.Should().NotBeNullOrWhiteSpace();
            keycloakAuthResponse
                .Subject.RefreshToken.Should().NotBeNullOrWhiteSpace();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "bearer", keycloakAuthResponse.Subject.AccessToken);
        }

        public void Dispose()
        {
            HttpClient.Dispose();
        }
    }
}
