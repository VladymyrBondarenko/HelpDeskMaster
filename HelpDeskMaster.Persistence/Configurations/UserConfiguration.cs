using HelpDeskMaster.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.OwnsOne(x => x.Login, loginBuilder =>
            {
                loginBuilder.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(254);

                loginBuilder.HasIndex(x => x.Value).IsUnique();
            });
        }
    }
}
