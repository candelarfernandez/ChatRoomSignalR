﻿using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace ChatRoom
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string room, string user, string message)
        {
            await Clients.Group(room).SendAsync("RecieveMessage", user, message);

        }
        public async Task AddToGroup(string room)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room).SendAsync("ShowWho", $"{Context.ConnectionId} se unió a esta subasta");

        }
        public async Task SendOferta(string room, decimal monto, int? idComprador)
        {
            await Clients.Group(room).SendAsync("ReceiveOferta", monto, idComprador);
        }
    }
}