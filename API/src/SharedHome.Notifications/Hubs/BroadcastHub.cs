using Microsoft.AspNetCore.SignalR;
using SharedHome.Notifications.Services;

namespace SharedHome.Notifications.Hubs
{
    public class BroadcastHub : Hub<IHubClient>
    {
        public string GetConnectionId() => Context.ConnectionId;
    }
}
