using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Persistence.Configurations
{
    public class AnswerConfig: IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Content).IsRequired();
            builder.Property(a => a.UserId).IsRequired();
            builder.Property(a => a.UserName).IsRequired().HasMaxLength(150);
            builder.Property(a => a.Created).IsRequired();
        }
    }
}