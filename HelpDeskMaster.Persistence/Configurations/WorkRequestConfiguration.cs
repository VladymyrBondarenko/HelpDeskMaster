using HelpDeskMaster.Domain.Entities.WorkRequests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class WorkRequestConfiguration : IEntityTypeConfiguration<WorkRequest>
    {
        public void Configure(EntityTypeBuilder<WorkRequest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Content)
                .HasMaxLength(254);

            builder.Property(x => x.DesiredExecutionDate)
                .IsRequired();

            builder.Property(x => x.FailureRevealedDate)
                .IsRequired();

            builder.Property(x => x.WorkCategoryId)
                .IsRequired();

            builder.Property(x => x.WorkDirectionId)
                .IsRequired();
        }
    }
}
