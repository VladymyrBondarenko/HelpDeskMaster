using DotNet.Testcontainers.Builders;
using HelpDeskMaster.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Testcontainers.Keycloak;
using Testcontainers.PostgreSql;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    internal class HdmContainersInitializer
    {
        private PostgreSqlContainer _keycloakPostgresContainer;
        public PostgreSqlContainer KeycloakPostgresContainer => _keycloakPostgresContainer;


        public const int KeycloakContainerPort = 8080;

        private KeycloakContainer _keycloakContainer;
        public KeycloakContainer KeycloakContainer => _keycloakContainer;


        private PostgreSqlContainer _hdmPostgresContainer;
        public PostgreSqlContainer HdmPostgresContainer => _hdmPostgresContainer;

        public HdmContainersInitializer()
        {
            var network = new NetworkBuilder()
                .WithName(Guid.NewGuid().ToString())
                .Build();

            var keycloakContainerName = $"keycloak-test-{Guid.NewGuid()}";
            var keycloakPgContainerName = $"keycloak-postgres-test-{Guid.NewGuid()}";
            var hdmPgContainerName = $"hdm-postgres-test-{Guid.NewGuid()}";

            _keycloakPostgresContainer = new PostgreSqlBuilder()
               .WithImage("postgres:16.2")
               .WithName(keycloakPgContainerName)
               .WithPortBinding(5432, true)
               .WithDatabase("keycloakdb")
               .WithUsername("admin")
               .WithPassword("admin")
               .WithNetwork(network)
               .Build();

            _keycloakContainer = new KeycloakBuilder()
                .WithImage("quay.io/keycloak/keycloak:24.0")
                .WithName(keycloakContainerName)
                .WithNetwork(network)
                .WithEnvironment("KC_HTTP_ENABLED", "true")
                .WithEnvironment("KC_HTTP_PORT", KeycloakContainerPort.ToString())
                .WithEnvironment("KEYCLOAK_ADMIN", "admin")
                .WithEnvironment("KEYCLOAK_ADMIN_PASSWORD", "admin")
                .WithEnvironment("KC_DB_URL_HOST", keycloakPgContainerName)
                .WithEnvironment("KC_DB_URL_PORT", "5432")
                .WithEnvironment("KC_DB_URL_DATABASE", "keycloakdb")
                .WithEnvironment("KC_DB_USERNAME", "admin")
                .WithEnvironment("KC_DB_PASSWORD", "admin")
                .WithEnvironment("KC_DB_SCHEMA", "public")
                .WithResourceMapping(
                    "Authentication/Keycloak/ImportForTests/UserFullAccess",
                    "/opt/keycloak/tmp/import")
                .WithCommand("-Dkeycloak.migration.action=import")
                .WithCommand("-Dkeycloak.migration.provider=dir")
                .WithCommand("-Dkeycloak.migration.dir=/opt/keycloak/tmp/import")
                .WithCommand("-Dkeycloak.migration.strategy=OVERWRITE_EXISTING")
                .WithPortBinding(KeycloakContainerPort, true)
                .Build();

            _hdmPostgresContainer = new PostgreSqlBuilder()
                .WithImage("postgres:16.2")
                .WithName(hdmPgContainerName)
                .WithPortBinding(5432, true)
                .WithNetwork(network)
                .Build();
        }

        public async Task InitializeAsync()
        {
            await _hdmPostgresContainer.StartAsync();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseNpgsql(_hdmPostgresContainer.GetConnectionString());

            var dbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
            await dbContext.Database.MigrateAsync();

            await _keycloakPostgresContainer.StartAsync();
            await _keycloakContainer.StartAsync();
        }

        public async Task DisposeAsync()
        {
            await _hdmPostgresContainer.DisposeAsync();
            await _keycloakPostgresContainer.DisposeAsync();
            await _keycloakContainer.DisposeAsync();
        }
    }
}
