using BolaSocial.Api.Domain.Entities;
using BolaSocial.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/teams")]
public sealed class TeamsController : ControllerBase
{
    private readonly AppDbContext _db;

    public TeamsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMyTeams()
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var teams = await _db.Teams.AsNoTracking().Where(t => t.UserId == userId)
            .Select(t => new { t.Id, t.Name, t.KitPrimary, t.KitSecondary, t.LogoUrl })
            .ToListAsync();
        return Ok(teams);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequest request)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var team = new Team
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = request.Name,
            KitPrimary = request.KitPrimary,
            KitSecondary = request.KitSecondary,
            LogoUrl = request.LogoUrl
        };

        _db.Teams.Add(team);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMyTeams), new { id = team.Id }, team);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateTeam(Guid id, [FromBody] UpdateTeamRequest request)
    {
        if (!TryGetUserId(out var userId))
        {
            return Unauthorized();
        }

        var team = await _db.Teams.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        if (team is null)
        {
            return NotFound();
        }

        team.Name = request.Name ?? team.Name;
        team.KitPrimary = request.KitPrimary ?? team.KitPrimary;
        team.KitSecondary = request.KitSecondary ?? team.KitSecondary;
        team.LogoUrl = request.LogoUrl ?? team.LogoUrl;

        await _db.SaveChangesAsync();
        return NoContent();
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

    public record CreateTeamRequest(string Name, string KitPrimary, string KitSecondary, string LogoUrl);
    public record UpdateTeamRequest(string? Name, string? KitPrimary, string? KitSecondary, string? LogoUrl);
}
