namespace BolaSocial.Api.Domain.Entities;

public sealed class Match
{
    public Guid Id { get; set; }
    public string Mode { get; set; } = "1v1";
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public Guid? HomeTeamId { get; set; }
    public Guid? AwayTeamId { get; set; }
    public string ResultJson { get; set; } = "{}";
    public string? ReplayRef { get; set; }
}
