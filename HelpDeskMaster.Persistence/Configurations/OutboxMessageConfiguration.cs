using HelpDeskMaster.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
    {
        public void Configure(EntityTypeBuilder<OutboxMessage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OccuredOnUtc).IsRequired();
            builder.Property(x => x.OccuredOnUtc).IsRequired();

            builder.Property(x => x.Type)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
