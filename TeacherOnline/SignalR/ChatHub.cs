using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TeacherOnline.BLL.SignalR
{
    [Authorize]
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message, string ToUserName) 
        {
            if(Context.UserIdentifier is string UserName)
            {
                await Clients.Users(ToUserName, UserName).SendAsync("ReceiveMessage", message, UserName);
            }
        }
        public Task JoinRoom(string roomId)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public Task LeaveRoom(string roomId)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Others.SendAsync("Notify", $"Пользователь {Context.UserIdentifier} в сети");
            await base.OnConnectedAsync();
        }
        
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await Clients.Others.SendAsync("Notify", $"Пользователь {Context.UserIdentifier} не в сети");
            await base.OnDisconnectedAsync(exception);
        }

    }
}
