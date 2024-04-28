using HelpDeskMaster.Domain.Entities.Equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class ComputerEquipmentConfiguration : IEntityTypeConfiguration<ComputerEquipment>
    {
        public void Configure(EntityTypeBuilder<ComputerEquipment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.EquipmentId).IsRequired();
            builder.Property(x => x.ComputerId).IsRequired();
            builder.Property(x => x.AssignedDate).IsRequired();

            builder.HasOne<Equipment>()
                .WithMany(x => x.ComputerEquipments)
                .HasForeignKey(x => x.ComputerId);
        }
    }
}
