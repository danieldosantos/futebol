namespace BolaSocial.Api.Domain.Entities;

public sealed class Team
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string KitPrimary { get; set; } = "#1e3a8a";
    public string KitSecondary { get; set; } = "#f97316";
    public string LogoUrl { get; set; } = string.Empty;

    public User? User { get; set; }
    public ICollection<Player> Players { get; set; } = new List<Player>();
}
