using System.Threading;
using System.Threading.Tasks;
using MediatR;
using QuestionAndAnswer.Application.Common.Interfaces;

namespace QuestionAndAnswer.Application.Common.Handlers
{
    public class AnswerNotificationHandler: INotificationHandler<CreateAnswerNotification>
    {
        private readonly IHubService _hubService;
        //private readonly IHubContext<QuestionsHub> _hubContext;

        public AnswerNotificationHandler(/*IHubContext<QuestionsHub> hubContext*/ IHubService hubService)
        {
            _hubService = hubService;
        }
        
        public Task Handle(CreateAnswerNotification notification, CancellationToken cancellationToken)
        {
            return _hubService.SendAsync(notification.QuestionId/*, notification.AnswerId*/);
        }
    }
    
    public class CreateAnswerNotification : INotification
    {
        public int QuestionId { get; set; }
    }
}