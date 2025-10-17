namespace BolaSocial.Api.Domain.Entities;

public sealed class Friendship
{
    public Guid UserId { get; set; }
    public Guid FriendUserId { get; set; }
    public string Status { get; set; } = "pending";
}
