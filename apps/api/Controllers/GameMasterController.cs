using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Authorize(Roles = "gm")]
[Route("api/gm")]
public sealed class GameMasterController : ControllerBase
{
    [HttpGet("rooms")]
    public IActionResult GetRooms()
    {
        return Ok(new[] { new { id = "dev-room", players = 0, bots = 0, tickRate = 60 } });
    }

    [HttpPost("bots")]
    public IActionResult SpawnBot([FromBody] SpawnBotRequest request)
    {
        return Accepted(new { status = "bot-spawn-requested", request.MatchId });
    }

    [HttpPost("rooms/{id}/end")]
    public IActionResult EndRoom(string id)
    {
        return Accepted(new { status = "ending", id });
    }

    public record SpawnBotRequest(string MatchId, string Difficulty);
}
