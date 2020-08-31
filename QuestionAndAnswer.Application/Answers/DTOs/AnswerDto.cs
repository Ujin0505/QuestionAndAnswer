using MediatR;

namespace QuestionAndAnswer.Application.Answers.Models
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string Created { get; set; }
        public int QuestionId { get; set; }
    }
}