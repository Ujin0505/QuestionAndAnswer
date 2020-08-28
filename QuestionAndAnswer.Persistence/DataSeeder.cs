using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using QuestionAndAnswer.Data.Entities;

namespace QuestionAndAnswer.Persistence
{
    internal static class DataSeeder
    {
        public static void Seed(ModelBuilder builder)
        {
            SeedQuestions(builder);
            SeedAnswers(builder);
        }

        
        /*VALUES(1, 'Why should I learn TypeScript?', 

            'TypeScript seems to be getting popular so I wondered whether it is worth my time learning it? What benefits does it give over JavaScript?',

            '1',

            'bob.test@test.com',

            '2019-05-18 14:32')*/
        
        private static void SeedQuestions(ModelBuilder builder)
        {
            builder.Entity<Question>().HasData(
                new Question()
                {
                    Id = -1,
                    Title = "Why should I learn TypeScript?",
                    Content =
                        "TypeScript seems to be getting popular so I wondered whether it is worth my time learning it? What benefits does it give over JavaScript?",
                    UserId = "1",
                    UserName = "bob.test@test.com",
                    Created = new DateTime(2019, 5, 18, 14, 32, 0)
                },
                new Question()
                {
                    Id = -2,
                    Title = "Which state management tool should I use?",
                    Content =
                        "There seem to be a fair few state management tools around for React - React, Unstated, ... Which one should I use?",
                    UserId = "2",
                    UserName = "jane.test@test.com",
                    Created = new DateTime(2019, 5, 18, 14, 48, 0)
                }
                );

        }
        
        private static void SeedAnswers(ModelBuilder builder)
        {
            builder.Entity<Answer>().HasData(
                new Answer()
                {
                    Id = -1,
                    QuestionId = -1,
                    Content =
                        "To catch problems earlier speeding up your developments",
                    UserId = "2",
                    UserName = "jane.test@test.com",
                    Created = new DateTime(2019, 5, 18, 14, 50, 0)
                },
                new Answer()
                {
                    Id = -2,
                    QuestionId = -1,
                    Content =
                        "So, that you can use the JavaScript features of tomorrow, today",
                    UserId = "3",
                    UserName = "fred.test@test.com",
                    Created = new DateTime(2019, 5, 18, 16, 48, 0)
                }
            );
            
        }
        
    }
}