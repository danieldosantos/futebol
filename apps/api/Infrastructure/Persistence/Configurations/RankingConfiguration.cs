using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BolaSocial.Api.Infrastructure.Persistence.Configurations;

public sealed class RankingConfiguration : IEntityTypeConfiguration<Ranking>
{
    public void Configure(EntityTypeBuilder<Ranking> builder)
    {
        builder.ToTable("rankings");
        builder.HasKey(x => x.TeamId);
        builder.Property(x => x.Mmr).HasDefaultValue(1000);
    }
}
