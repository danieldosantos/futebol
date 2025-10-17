using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BolaSocial.Api.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Team> Teams => Set<Team>();
    public DbSet<Player> Players => Set<Player>();
    public DbSet<Match> Matches => Set<Match>();
    public DbSet<Ranking> Rankings => Set<Ranking>();
    public DbSet<Friendship> Friendships => Set<Friendship>();
    public DbSet<ShopItem> ShopItems => Set<ShopItem>();
    public DbSet<Training> Trainings => Set<Training>();
    public DbSet<TelemetrySample> Telemetry => Set<TelemetrySample>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
