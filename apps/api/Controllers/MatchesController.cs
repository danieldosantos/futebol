using BolaSocial.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/matches")]
public sealed class MatchesController : ControllerBase
{
    private readonly AppDbContext _db;

    public MatchesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMatch(Guid id)
    {
        var match = await _db.Matches.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        if (match is null)
        {
            return NotFound();
        }
        return Ok(match);
    }

    [HttpGet("{id:guid}/events")]
    public async Task<IActionResult> GetMatchEvents(Guid id)
    {
        var events = await _db.Telemetry.AsNoTracking()
            .Where(t => t.MatchId == id)
            .OrderBy(t => t.Tick)
            .Take(1000)
            .ToListAsync();
        return Ok(events);
    }
}
