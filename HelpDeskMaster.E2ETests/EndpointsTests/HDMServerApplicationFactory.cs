using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class HdmServerApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly HdmContainersInitializer _containersInitializer = new();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var keycloakPublicPort = _containersInitializer.KeycloakContainer.GetMappedPublicPort(
                HdmContainersInitializer.KeycloakContainerPort);

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:HdmDbConnection"] = _containersInitializer.HdmPostgresContainer.GetConnectionString(),
                    ["Keycloak:realm"] = "hdm-realm",
                    ["Keycloak:resource"] = "hdm-client",
                    ["Keycloak:credentials:secret"] = "WNMzQVpMkjskGVTZCJB4T5SQ6xPQjJzg",
                    ["Keycloak:confidential-port"] = "0",
                    ["Keycloak:auth-server-url"] = $"http://localhost:{keycloakPublicPort}/",
                    ["Keycloak:verify-token-issuer"] = "False",
                    ["Keycloak:verify-token-audience"] = "False",
                    ["Keycloak:ssl-required"] = "none",
                    ["EmailSender:Server"] = "",
                    ["EmailSender:Port"] = "0",
                    ["EmailSender:SenderName"] = "",
                    ["EmailSender:SenderEmail"] = "",
                    ["EmailSender:UserName"] = "",
                    ["EmailSender:Password"] = "",
                    ["BackgroundJobs:Outbox:Schedule"] = "0/15 * * * * *",
                    ["StartOptions:GenerateTestData"] = "false"
                }).Build();
            builder.UseConfiguration(configuration);

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _containersInitializer.InitializeAsync();
        }

        public new async Task DisposeAsync()
        {
            await _containersInitializer.DisposeAsync();
        }
    }
}
