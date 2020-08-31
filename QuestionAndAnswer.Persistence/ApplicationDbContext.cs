using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using QuestionAndAnswer.Data.Entities;
using QuestionAndAnswer.Persistence.Configurations;

namespace QuestionAndAnswer.Persistence
{
    //dotnet-ef migrations add init --project .\QuestionAndAnswer.Persistence\QuestionAndAnswer.Persistence.csproj --startup-project .\QuestionAndAnswer\QuestionAndAnswer.csproj
    //dotnet-ef database update --project .\QuestionAndAnswer.Persistence\QuestionAndAnswer.Persistence.csproj --startup-project .\QuestionAndAnswer\QuestionAndAnswer.csproj-v
    
    public class ApplicationDbContext: DbContext
    {
        private readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //BuildQuestion(modelBuilder);
            //BuildAnswer(modelBuilder);
            modelBuilder.ApplyConfiguration(new QuestionConfig());
            modelBuilder.ApplyConfiguration(new AnswerConfig());
            
            DataSeeder.Seed(modelBuilder);
        }

        /*private void BuildQuestion(ModelBuilder builder)
        {
            builder.Entity<Question>(question =>
            {
                question.HasKey(q => q.Id);
                question.Property(q => q.Title).IsRequired().HasMaxLength(100);
                question.Property(q => q.Content).IsRequired();
                question.Property(q => q.UserId).IsRequired();
                question.Property(q => q.UserName).IsRequired().HasMaxLength(150);
                question.Property(q => q.Created).IsRequired();
                question.HasMany(q => q.Answers)
                    .WithOne(a => a.Question);
            });
        }*/
        /*private void BuildAnswer(ModelBuilder builder)
        {
            builder.Entity<Answer>(answer =>
            {
                answer.HasKey(a => a.Id);
                answer.Property(a => a.Content).IsRequired();
                answer.Property(a => a.UserId).IsRequired();
                answer.Property(a => a.UserName).IsRequired().HasMaxLength(150);
                answer.Property(a => a.Created).IsRequired();
            });
        }*/
    }
    
    
    
}