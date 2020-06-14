using Microsoft.EntityFrameworkCore.Migrations;

namespace JDR.Migrations
{
    public partial class RaceSousRace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeeStat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatId = table.Column<int>(nullable: true),
                    RaceId = table.Column<int>(nullable: true),
                    NbDee = table.Column<int>(nullable: false),
                    TailleDee = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeeStat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeeStat_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeeStat_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SousRace",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true),
                    RaceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousRace", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SousRace_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValeurRaceStat",
                columns: table => new
                {
                    StatId = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeurRaceStat", x => new { x.StatId, x.RaceId });
                    table.ForeignKey(
                        name: "FK_ValeurRaceStat_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValeurRaceStat_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValeurSousRaceStat",
                columns: table => new
                {
                    StatId = table.Column<int>(nullable: false),
                    SousRaceId = table.Column<int>(nullable: false),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeurSousRaceStat", x => new { x.StatId, x.SousRaceId });
                    table.ForeignKey(
                        name: "FK_ValeurSousRaceStat_SousRace_SousRaceId",
                        column: x => x.SousRaceId,
                        principalTable: "SousRace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValeurSousRaceStat_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeeStat_RaceId",
                table: "DeeStat",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeeStat_StatId",
                table: "DeeStat",
                column: "StatId");

            migrationBuilder.CreateIndex(
                name: "IX_SousRace_RaceId",
                table: "SousRace",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ValeurRaceStat_RaceId",
                table: "ValeurRaceStat",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ValeurSousRaceStat_SousRaceId",
                table: "ValeurSousRaceStat",
                column: "SousRaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeeStat");

            migrationBuilder.DropTable(
                name: "ValeurRaceStat");

            migrationBuilder.DropTable(
                name: "ValeurSousRaceStat");

            migrationBuilder.DropTable(
                name: "SousRace");

            migrationBuilder.DropTable(
                name: "Race");
        }
    }
}
