using App.Domain.Feedbacks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.Database.EntityTypeConfigurations
{
    public class FeedbackEntityConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.RecordStatus).IsRequired();
            builder.Property(x => x.Text).HasMaxLength(200).IsRequired();

            builder.HasOne(x => x.Project)
                .WithMany(x => x.Feedbacks);
        }
    }
}