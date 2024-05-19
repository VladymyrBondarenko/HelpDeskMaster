using HelpDeskMaster.App.DataMocking;
using HelpDeskMaster.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.WebApi.Helpers
{
    public static class DataPreparationHelper
    {
        public static void PrepareDataPopulation(this WebApplication app)
        {
            using var seviceScope = app.Services.CreateScope();

            var dbContext = seviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            if (dbContext != null)
            {
                ApplyMigrations(dbContext);

                if (app.Configuration.GetValue<bool>("StartOptions:GenerateTestData"))
                {
                    var mockService = seviceScope.ServiceProvider.GetService<IHdmDataMockService>();
                    mockService?.MockData().GetAwaiter().GetResult();
                }
            }
        }

        private static void ApplyMigrations(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Applying migration");

            dbContext.Database.Migrate();
        }
    }
}
