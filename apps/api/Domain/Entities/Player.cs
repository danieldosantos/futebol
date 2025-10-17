namespace BolaSocial.Api.Domain.Entities;

public sealed class Player
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = "MF";
    public int Pace { get; set; }
    public int Pass { get; set; }
    public int Shot { get; set; }
    public int Defense { get; set; }
    public int Control { get; set; }
    public int Stamina { get; set; }
    public int Overall { get; set; }

    public Team? Team { get; set; }
}
