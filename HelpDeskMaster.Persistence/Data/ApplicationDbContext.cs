using HelpDeskMaster.Domain.Entities.Users;
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

        public DbSet<User> Users { get; set; }

        public DbSet<UserEquipment> UserEquipments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}