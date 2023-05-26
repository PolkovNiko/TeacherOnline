using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace TeacherOnline.SignalR
{
    public class CustomerUserIdProvider : IUserIdProvider
    {
        public virtual string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.FindFirst(ClaimTypes.Email)?.Value!;
        }
    }
}
