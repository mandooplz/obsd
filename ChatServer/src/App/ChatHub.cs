using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using Core;

namespace App
{
    public class ChatHub : Hub
    {
        private readonly ChatRoomManager _chatRoomManager;

        public ChatHub(ChatRoomManager chatRoomManager)
        {
            _chatRoomManager = chatRoomManager;
        }

        public async Task CreateRoom(string roomName)
        {
            _chatRoomManager.CreateRoom(roomName);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has created and joined room {roomName}.");
        }

        public async Task JoinRoom(string roomName)
        {
            _chatRoomManager.JoinRoom(roomName, Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined room {roomName}.");
        }

        public async Task LeaveRoom(string roomName)
        {
            _chatRoomManager.LeaveRoom(roomName, Context.ConnectionId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
            await Clients.Group(roomName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left room {roomName}.");
        }

        public async Task SendMessageToRoom(string roomName, string message)
        {
            await Clients.Group(roomName).SendAsync("ReceiveMessage", message);
        }
    }
}
