namespace BolaSocial.Api.Domain.Entities;

public sealed class TelemetrySample
{
    public Guid Id { get; set; }
    public Guid MatchId { get; set; }
    public long Tick { get; set; }
    public string PayloadJson { get; set; } = "{}";
}
