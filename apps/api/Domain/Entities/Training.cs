namespace BolaSocial.Api.Domain.Entities;

public sealed class Training
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public string Attribute { get; set; } = "pace";
    public DateTime EndsAt { get; set; }
    public string State { get; set; } = "pending";
}
