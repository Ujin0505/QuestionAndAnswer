using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace QuestionAndAnswer.Infrastracture.Hubs
{
    public class QuestionsHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            await Clients.Caller.SendAsync("Message", "Successfully connected");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Caller.SendAsync("Message", "Successfully disconnected");
            await base.OnDisconnectedAsync(exception);
        }

        public async void SubscribeQuestion(int id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Question-{id}");
            await Clients.Caller.SendAsync("Message", "Successfully subscribe");
        }
        
        public async void UnSubscribeQuestion(int id)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Question-{id}");
            await Clients.Caller.SendAsync("Message", "Successfully unsubscribe");
        }
        
    }
}