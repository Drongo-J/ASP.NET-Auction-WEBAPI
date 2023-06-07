using Microsoft.AspNetCore.SignalR;

namespace WebApiSignalR.Hubs
{
    public class MessageHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public async Task LeaveRoom(string username)
        {
            await Clients.Others.SendAsync("UserExited", username);
        }

        public async Task SendMessage(string message)
        {
            await Clients.Others.SendAsync("ReceiveMessage", message + "'s Offer : ", FileHelper.Read());
        }

        public async Task SendWinnerMessage(string room, string username, string lastoffer)
        {
            await Clients.Others.SendAsync("ReceiveInfo", room, username, lastoffer);
        }

        public async Task JoinRoom(string room, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
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
