namespace BolaSocial.Api.Domain.Entities;

public sealed class ShopItem
{
    public Guid Id { get; set; }
    public string Kind { get; set; } = string.Empty;
    public int Price { get; set; }
    public string PayloadJson { get; set; } = "{}";
}
