using HelpDeskMaster.Persistence.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Testcontainers.PostgreSql;
using Xunit;

namespace HelpDeskMaster.E2ETests.EndpointsTests
{
    public class HDMServerApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder().Build();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:ApplicationDbConnection"] = _dbContainer.GetConnectionString()
                }).Build();
            builder.UseConfiguration(configuration);

            base.ConfigureWebHost(builder);
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseNpgsql(_dbContainer.GetConnectionString());

            var dbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);
            await dbContext.Database.MigrateAsync();
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.DisposeAsync();
        }
    }
}
