using HelpDeskMaster.Domain.Entities.Equipments;
using HelpDeskMaster.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class UserEquipmentConfiguration : IEntityTypeConfiguration<UserEquipment>
    {
        public void Configure(EntityTypeBuilder<UserEquipment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.AssignedDate).IsRequired();

            builder.HasOne<User>()
                .WithMany(x => x.Equipments)
                .HasForeignKey(x => x.UserId);
        }
    }
}
