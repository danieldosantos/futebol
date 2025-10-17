namespace BolaSocial.Api.Domain.Entities;

public sealed class Ranking
{
    public Guid TeamId { get; set; }
    public double Mmr { get; set; }
    public int Wins { get; set; }
    public int Losses { get; set; }
    public int Draws { get; set; }
    public int Streak { get; set; }
}
