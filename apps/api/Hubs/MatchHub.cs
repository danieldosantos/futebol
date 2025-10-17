using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BolaSocial.Api.Hubs;

[Authorize]
public sealed class MatchHub : Hub
{
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    public Task JoinMatch(string matchId)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, $"match:{matchId}");
    }

    public Task Input(string matchId, object payload)
    {
        // Placeholder: match loop real armazenaria input em fila.
        return Clients.Group($"match:{matchId}").SendAsync("inputAck", payload);
    }
}
