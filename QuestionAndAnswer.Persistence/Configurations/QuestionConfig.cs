using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Persistence.Configurations
{
    public class QuestionConfig: IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(q => q.Id);
            builder.Property(q => q.Title).IsRequired().HasMaxLength(100);
            builder.Property(q => q.Content).IsRequired();
            builder.Property(q => q.UserId).IsRequired();
            builder.Property(q => q.UserName).IsRequired().HasMaxLength(150);
            builder.Property(q => q.Created).IsRequired();
            builder.HasMany(q => q.Answers)
                .WithOne(a => a.Question);

            /*question.HasOne(q => q.Answer)
                .WithOne(a => a.Question)
                .HasForeignKey<Answer>(a => a.QuestionId);*/
        }
    }
}