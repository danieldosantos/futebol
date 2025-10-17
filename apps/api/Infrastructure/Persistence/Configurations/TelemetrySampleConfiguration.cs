using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BolaSocial.Api.Infrastructure.Persistence.Configurations;

public sealed class TelemetrySampleConfiguration : IEntityTypeConfiguration<TelemetrySample>
{
    public void Configure(EntityTypeBuilder<TelemetrySample> builder)
    {
        builder.ToTable("telemetry");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PayloadJson).HasColumnType("jsonb");
    }
}
