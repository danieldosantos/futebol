using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BolaSocial.Api.Infrastructure.Persistence.Configurations;

public sealed class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder.ToTable("teams");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(80);
        builder.Property(x => x.KitPrimary).HasMaxLength(16);
        builder.Property(x => x.KitSecondary).HasMaxLength(16);
        builder.Property(x => x.LogoUrl).HasMaxLength(200);
        builder.HasOne(x => x.User)
            .WithMany(x => x.Teams)
            .HasForeignKey(x => x.UserId);
    }
}
