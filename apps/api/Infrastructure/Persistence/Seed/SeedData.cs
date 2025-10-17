using System.Data.Common;
using System.Net.Sockets;
using BolaSocial.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BolaSocial.Api.Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static async Task EnsureSeedAsync(IServiceProvider services, CancellationToken cancellationToken = default)
    {
        using var scope = services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

        try
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync(cancellationToken);

            if (await context.Users.AnyAsync(cancellationToken))
            {
                logger.LogInformation("Seed skipped; users already exist.");
                return;
            }

            var now = DateTime.UtcNow;
            var gm = new User
            {
                Id = Guid.NewGuid(),
                Email = "gm@bolasocial.gg",
                PasswordHash = "SEED_HASH",
                Nickname = "GM",
                Role = Roles.GameMaster,
                CreatedAt = now
            };

            var azul = new User
            {
                Id = Guid.NewGuid(),
                Email = "azul@bolasocial.gg",
                PasswordHash = "SEED_HASH",
                Nickname = "JogadorAzul",
                Role = Roles.User,
                CreatedAt = now
            };

            var rosa = new User
            {
                Id = Guid.NewGuid(),
                Email = "rosa@bolasocial.gg",
                PasswordHash = "SEED_HASH",
                Nickname = "JogadorRosa",
                Role = Roles.User,
                CreatedAt = now
            };

            context.Users.AddRange(gm, azul, rosa);

            var azulTeam = CreateTeam(azul.Id, "Azul FC");
            var rosaTeam = CreateTeam(rosa.Id, "Rosa United");

            context.Teams.AddRange(azulTeam.Team, rosaTeam.Team);
            context.Players.AddRange(azulTeam.Players);
            context.Players.AddRange(rosaTeam.Players);

            context.Rankings.AddRange(
                new Ranking { TeamId = azulTeam.Team.Id, Mmr = 1000, Wins = 0, Losses = 0, Draws = 0, Streak = 0 },
                new Ranking { TeamId = rosaTeam.Team.Id, Mmr = 1000, Wins = 0, Losses = 0, Draws = 0, Streak = 0 }
            );

            context.ShopItems.AddRange(
                new ShopItem { Id = Guid.NewGuid(), Kind = "kit", Price = 500, PayloadJson = "{\"color\":\"#2563eb\"}" },
                new ShopItem { Id = Guid.NewGuid(), Kind = "badge", Price = 750, PayloadJson = "{\"icon\":\"star\"}" }
            );

            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seed completed.");
        }
        catch (Exception ex) when (IsDatabaseUnavailable(ex))
        {
            logger.LogWarning(ex, "Skipping database seed because the database is unavailable.");
        }
    }

    private static (Team Team, List<Player> Players) CreateTeam(Guid userId, string name)
    {
        var teamId = Guid.NewGuid();
        var team = new Team
        {
            Id = teamId,
            UserId = userId,
            Name = name,
            KitPrimary = "#2563eb",
            KitSecondary = "#fbbf24"
        };

        var players = Enumerable.Range(1, 10).Select(i => new Player
        {
            Id = Guid.NewGuid(),
            TeamId = teamId,
            Name = $"Jogador {i}",
            Position = i switch
            {
                1 => "GK",
                2 or 3 => "DF",
                4 or 5 or 6 => "MF",
                _ => "FW"
            },
            Pace = 60 + i,
            Pass = 55 + i,
            Shot = 50 + i,
            Defense = 50 + i,
            Control = 58 + i,
            Stamina = 65 + i,
            Overall = 60 + i
        }).ToList();

        return (team, players);
    }

    private static bool IsDatabaseUnavailable(Exception ex)
    {
        return ex is DbException || ex.GetBaseException() is SocketException;
    }
}
