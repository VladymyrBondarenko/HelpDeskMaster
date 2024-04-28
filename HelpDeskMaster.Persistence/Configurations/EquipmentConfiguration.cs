using HelpDeskMaster.Domain.Entities.Equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class EquipmentConfiguration : IEntityTypeConfiguration<Equipment>
    {
        public void Configure(EntityTypeBuilder<Equipment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.EquipmentTypeId).IsRequired();

            builder.Property(x => x.Model)
                .HasMaxLength(30);

            builder.Property(x => x.FactoryNumber)
                .HasMaxLength(30);

            builder.HasMany<ComputerEquipment>()
                .WithOne(x => x.Equipment)
                .HasForeignKey(x => x.EquipmentId);
        }
    }
}
