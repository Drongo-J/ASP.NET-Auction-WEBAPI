using Microsoft.AspNetCore.SignalR;
using WebApiSignalR.Models;

namespace WebApiSignalR.Hubs
{
    public class MessageHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public async Task LeaveRoom(string room, string username)
        {
            ConnectedUsers.GroupUserCounts[room] -= 1;
            await Clients.Others.SendAsync("UserExited", username);
        }

        //public async Task SendMessage(string room, string message)
        //{
        //    await Clients.OthersInGroup(room).SendAsync("ReceiveMessage", message + "'s Offer : ", FileHelper.Read());
        //}

        public async Task SendWinnerMessage(string room, string username, string lastoffer)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveInfo", room, username, lastoffer);
        }

        public async Task SendUserMessage(string room, string username, string message)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveUserMessage", username, message);
        }

        public async Task JoinRoom(string room, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            if (ConnectedUsers.GroupUserCounts.ContainsKey(room))
                ConnectedUsers.GroupUserCounts[room] += 1;
            else
                ConnectedUsers.GroupUserCounts.Add(room, 1);
            await Clients.OthersInGroup(room).SendAsync("ReceiveJoinInfo", user);
        }

        public async Task SendOffer(string room, string message)
        {
            var value = FileHelper.Read(room);
            await Clients.OthersInGroup(room).SendAsync("RecieveRoomMessage", message, value);
        }

        public async Task SendWinnerMessageRoom(string room, string message)
        {
            await Clients.OthersInGroup(room).SendAsync("ReceiveInfoRoom", message, FileHelper.Read(room));
        }
    }
}
