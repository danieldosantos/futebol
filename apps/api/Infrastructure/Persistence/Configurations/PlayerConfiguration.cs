using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BolaSocial.Api.Infrastructure.Persistence.Configurations;

public sealed class PlayerConfiguration : IEntityTypeConfiguration<Player>
{
    public void Configure(EntityTypeBuilder<Player> builder)
    {
        builder.ToTable("players");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
        builder.Property(x => x.Position).HasMaxLength(4);
        builder.HasOne(x => x.Team)
            .WithMany(x => x.Players)
            .HasForeignKey(x => x.TeamId);
    }
}
