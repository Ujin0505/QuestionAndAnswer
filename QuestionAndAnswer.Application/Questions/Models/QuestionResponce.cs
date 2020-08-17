using MediatR;

namespace QuestionAndAnswer.Application.Models
{
    public class QuestionResponce: INotification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string DateCreated { get; set; }
    }
}