using System;
using System.Collections.Generic;

namespace QuestionAndAnswer.Data.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}