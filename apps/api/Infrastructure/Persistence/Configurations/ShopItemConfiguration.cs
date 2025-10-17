using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BolaSocial.Api.Infrastructure.Persistence.Configurations;

public sealed class ShopItemConfiguration : IEntityTypeConfiguration<ShopItem>
{
    public void Configure(EntityTypeBuilder<ShopItem> builder)
    {
        builder.ToTable("shop_items");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Kind).HasMaxLength(50);
        builder.Property(x => x.PayloadJson).HasColumnType("jsonb");
    }
}
