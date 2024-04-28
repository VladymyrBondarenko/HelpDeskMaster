using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.EquipmentTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class EquipmentTypeConfiguration : IEntityTypeConfiguration<EquipmentType>
    {
        public void Configure(EntityTypeBuilder<EquipmentType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.TypeOfEquipment);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(75);

            builder.HasMany<Equipment>()
                .WithOne(x => x.EquipmentType)
                .HasForeignKey(x => x.EquipmentTypeId);
        }
    }
}