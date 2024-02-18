using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Domain.Entities.WorkDirections;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<WorkCategory> WorkCategories { get; set; }

        public DbSet<WorkDirection> WorkDirections { get; set; }
    }
}
