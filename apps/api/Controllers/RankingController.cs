using BolaSocial.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/ranking")]
public sealed class RankingController : ControllerBase
{
    private readonly AppDbContext _db;

    public RankingController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("global")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGlobal()
    {
        var ranking = await _db.Rankings.AsNoTracking()
            .OrderByDescending(r => r.Mmr)
            .Take(100)
            .ToListAsync();
        return Ok(ranking);
    }

    [HttpGet("friends")]
    public async Task<IActionResult> GetFriends()
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var friendIds = await _db.Friendships.Where(f => f.UserId == userId && f.Status == "accepted")
            .Select(f => f.FriendUserId)
            .ToListAsync();

        var teamIds = await _db.Teams.Where(t => friendIds.Contains(t.UserId))
            .Select(t => t.Id)
            .ToListAsync();

        var ranking = await _db.Rankings.AsNoTracking()
            .Where(r => teamIds.Contains(r.TeamId))
            .ToListAsync();

        return Ok(ranking);
    }

    private bool TryGetUserId(out Guid userId)
    {
        var userIdClaim = User.Claims.SingleOrDefault(c => c.Type.EndsWith("nameidentifier"))?.Value
            ?? User.Claims.SingleOrDefault(c => c.Type == "sub")?.Value;
        if (Guid.TryParse(userIdClaim, out userId))
        {
            return true;
        }
        userId = Guid.Empty;
        return false;
    }
}
