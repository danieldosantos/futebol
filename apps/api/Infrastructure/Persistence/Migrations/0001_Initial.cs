using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BolaSocial.Api.Infrastructure.Persistence.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Email = table.Column<string>(type: "character varying(160)", maxLength: 160, nullable: false),
                PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                GoogleSub = table.Column<string>(type: "text", nullable: true),
                Nickname = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                Role = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_users", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "shop_items",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Kind = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Price = table.Column<int>(type: "integer", nullable: false),
                PayloadJson = table.Column<string>(type: "jsonb", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_shop_items", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "teams",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                KitPrimary = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                KitSecondary = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                LogoUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_teams", x => x.Id);
                table.ForeignKey(
                    name: "FK_teams_users_UserId",
                    column: x => x.UserId,
                    principalTable: "users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "matches",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Mode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                HomeTeamId = table.Column<Guid>(type: "uuid", nullable: true),
                AwayTeamId = table.Column<Guid>(type: "uuid", nullable: true),
                ResultJson = table.Column<string>(type: "jsonb", nullable: false),
                ReplayRef = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_matches", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "rankings",
            columns: table => new
            {
                TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                Mmr = table.Column<double>(type: "double precision", nullable: false, defaultValue: 1000.0),
                Wins = table.Column<int>(type: "integer", nullable: false),
                Losses = table.Column<int>(type: "integer", nullable: false),
                Draws = table.Column<int>(type: "integer", nullable: false),
                Streak = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_rankings", x => x.TeamId);
            });

        migrationBuilder.CreateTable(
            name: "telemetry",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                MatchId = table.Column<Guid>(type: "uuid", nullable: false),
                Tick = table.Column<long>(type: "bigint", nullable: false),
                PayloadJson = table.Column<string>(type: "jsonb", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_telemetry", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "trainings",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                Attribute = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                EndsAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                State = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_trainings", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "friends",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                FriendUserId = table.Column<Guid>(type: "uuid", nullable: false),
                Status = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_friends", x => new { x.UserId, x.FriendUserId });
            });

        migrationBuilder.CreateTable(
            name: "players",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                Position = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                Pace = table.Column<int>(type: "integer", nullable: false),
                Pass = table.Column<int>(type: "integer", nullable: false),
                Shot = table.Column<int>(type: "integer", nullable: false),
                Defense = table.Column<int>(type: "integer", nullable: false),
                Control = table.Column<int>(type: "integer", nullable: false),
                Stamina = table.Column<int>(type: "integer", nullable: false),
                Overall = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_players", x => x.Id);
                table.ForeignKey(
                    name: "FK_players_teams_TeamId",
                    column: x => x.TeamId,
                    principalTable: "teams",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_players_TeamId",
            table: "players",
            column: "TeamId");

        migrationBuilder.CreateIndex(
            name: "IX_teams_UserId",
            table: "teams",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_users_Email",
            table: "users",
            column: "Email",
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "friends");
        migrationBuilder.DropTable(name: "matches");
        migrationBuilder.DropTable(name: "players");
        migrationBuilder.DropTable(name: "rankings");
        migrationBuilder.DropTable(name: "shop_items");
        migrationBuilder.DropTable(name: "telemetry");
        migrationBuilder.DropTable(name: "trainings");
        migrationBuilder.DropTable(name: "teams");
        migrationBuilder.DropTable(name: "users");
    }
}
