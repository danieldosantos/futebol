using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BolaSocial.Api.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired().HasMaxLength(160);
        builder.Property(x => x.PasswordHash).HasMaxLength(256);
        builder.Property(x => x.Nickname).HasMaxLength(60);
        builder.Property(x => x.Role).HasMaxLength(32);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
    }
}
