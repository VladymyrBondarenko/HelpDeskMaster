using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using HelpDeskMaster.Domain.Entities.Users;
using HelpDeskMaster.Domain.Entities.WorkCategories;
using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using HelpDeskMaster.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskMaster.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<WorkRequest> WorkRequests { get; set; }

        public DbSet<WorkCategory> WorkCategories { get; set; }

        public DbSet<WorkDirection> WorkDirections { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserEquipment> UserEquipments { get; set; }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        public DbSet<ComputerEquipment> ComputerEquipments { get; set; }

        public DbSet<EquipmentComputerInfo> EquipmentComputerInfos { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}