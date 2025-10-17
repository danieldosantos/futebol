using BolaSocial.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/users")]
public sealed class UsersController : ControllerBase
{
    private readonly AppDbContext _db;

    public UsersController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        var userIdClaim = User.Claims.SingleOrDefault(c => c.Type.EndsWith("nameidentifier"))?.Value
            ?? User.Claims.SingleOrDefault(c => c.Type == "sub")?.Value;
        if (userIdClaim is null)
        {
            return Unauthorized();
        }

        var userId = Guid.Parse(userIdClaim);
        var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
        {
            return NotFound();
        }

        return Ok(new
        {
            user.Id,
            user.Email,
            user.Nickname,
            user.Role,
            user.CreatedAt,
            user.LastLoginAt
        });
    }
}
