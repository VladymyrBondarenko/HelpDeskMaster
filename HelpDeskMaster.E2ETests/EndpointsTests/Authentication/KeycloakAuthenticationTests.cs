using FluentAssertions;
using HelpDeskMaster.Infrastracture.Authentication.KeycloakAuth.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

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
            var keycloakAuthResponse = await _keycloakAuthService
                .Invoking(x => x.AuthenticateToKeycloak())
                .Should().NotThrowAsync();

            keycloakAuthResponse
                .Subject.Should().NotBeNull();
            keycloakAuthResponse
                .Subject.AccessToken.Should().NotBeNullOrWhiteSpace();
            keycloakAuthResponse
                .Subject.RefreshToken.Should().NotBeNullOrWhiteSpace();
        }
    }
}
