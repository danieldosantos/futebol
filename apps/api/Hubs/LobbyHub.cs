using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BolaSocial.Api.Hubs;

[Authorize]
public sealed class LobbyHub : Hub
{
    public Task WatchQueue()
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, "matchmaking");
    }
}
