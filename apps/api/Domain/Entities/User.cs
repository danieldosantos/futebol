namespace BolaSocial.Api.Domain.Entities;

public sealed class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string? GoogleSub { get; set; }
    public string Nickname { get; set; } = string.Empty;
    public string Role { get; set; } = Roles.User;
    public DateTime CreatedAt { get; set; }
    public DateTime? LastLoginAt { get; set; }

    public ICollection<Team> Teams { get; set; } = new List<Team>();
}

public static class Roles
{
    public const string User = "user";
    public const string GameMaster = "gm";
}
