using HelpDeskMaster.Domain.Entities.WorkDirections;
using HelpDeskMaster.Domain.Entities.WorkRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class WorkDirectionConfiguration : IEntityTypeConfiguration<WorkDirection>
    {
        public void Configure(EntityTypeBuilder<WorkDirection> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(75);

            builder.HasMany<WorkRequest>()
                .WithOne(x => x.WorkDirection)
                .HasForeignKey(x => x.WorkDirectionId);
        }
    }
}
