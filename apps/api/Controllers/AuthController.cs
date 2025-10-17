using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BolaSocial.Api.Domain.Entities;
using BolaSocial.Api.Infrastructure;
using BolaSocial.Api.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BolaSocial.Api.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTimeProvider _clock;

    public AuthController(AppDbContext db, IOptions<JwtOptions> jwtOptions, IDateTimeProvider clock)
    {
        _db = db;
        _jwtOptions = jwtOptions.Value;
        _clock = clock;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        if (await _db.Users.AnyAsync(x => x.Email == request.Email))
        {
            return Conflict(new { message = "Email already registered" });
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = "HASH_PLACEHOLDER",
            Nickname = request.Nickname,
            CreatedAt = _clock.UtcNow,
            Role = Roles.User
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return await Login(new LoginRequest { Email = request.Email, Password = request.Password });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _db.Users.SingleOrDefaultAsync(x => x.Email == request.Email);
        if (user is null)
        {
            return Unauthorized();
        }

        // TODO: validate password hash (placeholder)
        var token = IssueToken(user);
        return new AuthResponse(token, "refresh-placeholder");
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public ActionResult<AuthResponse> Refresh([FromBody] RefreshRequest request)
    {
        // TODO: validate refresh token storage
        return Ok(new AuthResponse("token-placeholder", request.RefreshToken));
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        return NoContent();
    }

    private string IssueToken(User user)
    {
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)), SecurityAlgorithms.HmacSha256);
        var handler = new JwtSecurityTokenHandler();
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = _clock.UtcNow.AddMinutes(_jwtOptions.AccessTokenMinutes),
            SigningCredentials = credentials
        };
        var token = handler.CreateToken(descriptor);
        return handler.WriteToken(token);
    }

    public record RegisterRequest(string Email, string Password, string Nickname);
    public record LoginRequest(string Email, string Password);
    public record RefreshRequest(string RefreshToken);
    public record AuthResponse(string AccessToken, string RefreshToken);
}
