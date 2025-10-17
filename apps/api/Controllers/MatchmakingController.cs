using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/matchmaking")]
public sealed class MatchmakingController : ControllerBase
{
    [HttpPost("queue")]
    public IActionResult Queue()
    {
        return Accepted(new { status = "queued" });
    }

    [HttpDelete("queue")]
    public IActionResult Dequeue()
    {
        return Accepted(new { status = "removed" });
    }
}
