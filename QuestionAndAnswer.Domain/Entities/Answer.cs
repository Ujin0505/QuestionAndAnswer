using System;

namespace QuestionAndAnswer.Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}