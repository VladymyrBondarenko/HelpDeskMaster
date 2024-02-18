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
                SeedData(dbContext);
            }
        }

        private static void SeedData(ApplicationDbContext dbContext)
        {
            Console.WriteLine("Applying migration");

            dbContext.Database.Migrate();

            Console.WriteLine("Data already exist");
        }
    }
}
