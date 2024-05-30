using ChatRoom.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatRoom
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string room, string user, string amount)
        {
            await Clients.Group(room).SendAsync("RecieveMessage", user, amount);

        }
        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("ShowWho", $"{Context.ConnectionId} se unió a esta subasta");

        }
    }
}
