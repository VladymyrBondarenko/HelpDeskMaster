using HelpDeskMaster.Domain.Entities.Equipments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class EquipmentComputerInfoConfiguration : IEntityTypeConfiguration<EquipmentComputerInfo>
    {
        public void Configure(EntityTypeBuilder<EquipmentComputerInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.NameInNet)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasOne<Equipment>()
                .WithOne(x => x.EquipmentComputerInfo)
                .HasForeignKey<EquipmentComputerInfo>(x => x.ComputerId);
        }
    }
}
