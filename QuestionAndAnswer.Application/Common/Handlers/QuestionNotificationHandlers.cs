using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Common.Interfaces;
using QuestionAndAnswer.Application.Models;

namespace QuestionAndAnswer.Application.Common.Handlers
{
    public class QuestionNotificationHandlers: INotificationHandler<QuestionResponce>, INotificationHandler<ChangeQuestionNotification>, INotificationHandler<GetQuestionNotification>
    {
        public Task Handle(QuestionResponce notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(ChangeQuestionNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(GetQuestionNotification notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

    public class GetQuestionNotification: INotification 
    {
        public int QuestionId { get; set; }
    }
    
    public class ChangeQuestionNotification: INotification 
    {
        public int QuestionId { get; set; }
    }
    
}