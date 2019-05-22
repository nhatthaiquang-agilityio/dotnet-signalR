using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SignalRSimpleChat.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string nick, string message)
        {
            await Clients.All.SendAsync("Send", nick, message);
        }
    }
}
