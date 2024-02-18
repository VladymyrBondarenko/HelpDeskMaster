using HelpDeskMaster.Domain.Entities.WorkCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HelpDeskMaster.Persistence.Configurations
{
    internal class WorkCategoryConfiguration : IEntityTypeConfiguration<WorkCategory>
    {
        public void Configure(EntityTypeBuilder<WorkCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(75);
        }
    }
}