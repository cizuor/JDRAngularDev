using Microsoft.EntityFrameworkCore.Migrations;

namespace JDR.Migrations
{
    public partial class perso_classe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeeStat_Race_RaceId",
                table: "DeeStat");

            migrationBuilder.DropForeignKey(
                name: "FK_DeeStat_Stat_StatId",
                table: "DeeStat");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeeStat",
                table: "DeeStat");

            migrationBuilder.DropIndex(
                name: "IX_DeeStat_StatId",
                table: "DeeStat");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DeeStat");

            migrationBuilder.AlterColumn<int>(
                name: "StatId",
                table: "DeeStat",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RaceId",
                table: "DeeStat",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeeStat",
                table: "DeeStat",
                columns: new[] { "StatId", "RaceId" });

            migrationBuilder.CreateTable(
                name: "Classe",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Perso",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Prenom = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true),
                    Vivant = table.Column<bool>(nullable: false),
                    RaceId = table.Column<int>(nullable: true),
                    SousRaceId = table.Column<int>(nullable: true),
                    ClasseId = table.Column<int>(nullable: true),
                    Lvl = table.Column<int>(nullable: false),
                    posX = table.Column<int>(nullable: false),
                    posY = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Perso_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Perso_Race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Perso_SousRace_SousRaceId",
                        column: x => x.SousRaceId,
                        principalTable: "SousRace",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValeurClasseStat",
                columns: table => new
                {
                    StatId = table.Column<int>(nullable: false),
                    ClasseId = table.Column<int>(nullable: false),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeurClasseStat", x => new { x.StatId, x.ClasseId });
                    table.ForeignKey(
                        name: "FK_ValeurClasseStat_Classe_ClasseId",
                        column: x => x.ClasseId,
                        principalTable: "Classe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValeurClasseStat_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValeurBuffStat",
                columns: table => new
                {
                    StatId = table.Column<int>(nullable: false),
                    PersoId = table.Column<int>(nullable: false),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeurBuffStat", x => new { x.StatId, x.PersoId });
                    table.ForeignKey(
                        name: "FK_ValeurBuffStat_Perso_PersoId",
                        column: x => x.PersoId,
                        principalTable: "Perso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValeurBuffStat_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValeurPersoStat",
                columns: table => new
                {
                    StatId = table.Column<int>(nullable: false),
                    PersoId = table.Column<int>(nullable: false),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValeurPersoStat", x => new { x.StatId, x.PersoId });
                    table.ForeignKey(
                        name: "FK_ValeurPersoStat_Perso_PersoId",
                        column: x => x.PersoId,
                        principalTable: "Perso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ValeurPersoStat_Stat_StatId",
                        column: x => x.StatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perso_ClasseId",
                table: "Perso",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_Perso_RaceId",
                table: "Perso",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Perso_SousRaceId",
                table: "Perso",
                column: "SousRaceId");

            migrationBuilder.CreateIndex(
                name: "IX_ValeurBuffStat_PersoId",
                table: "ValeurBuffStat",
                column: "PersoId");

            migrationBuilder.CreateIndex(
                name: "IX_ValeurClasseStat_ClasseId",
                table: "ValeurClasseStat",
                column: "ClasseId");

            migrationBuilder.CreateIndex(
                name: "IX_ValeurPersoStat_PersoId",
                table: "ValeurPersoStat",
                column: "PersoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeeStat_Race_RaceId",
                table: "DeeStat",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeeStat_Stat_StatId",
                table: "DeeStat",
                column: "StatId",
                principalTable: "Stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeeStat_Race_RaceId",
                table: "DeeStat");

            migrationBuilder.DropForeignKey(
                name: "FK_DeeStat_Stat_StatId",
                table: "DeeStat");

            migrationBuilder.DropTable(
                name: "ValeurBuffStat");

            migrationBuilder.DropTable(
                name: "ValeurClasseStat");

            migrationBuilder.DropTable(
                name: "ValeurPersoStat");

            migrationBuilder.DropTable(
                name: "Perso");

            migrationBuilder.DropTable(
                name: "Classe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeeStat",
                table: "DeeStat");

            migrationBuilder.AlterColumn<int>(
                name: "RaceId",
                table: "DeeStat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "StatId",
                table: "DeeStat",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DeeStat",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeeStat",
                table: "DeeStat",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DeeStat_StatId",
                table: "DeeStat",
                column: "StatId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeeStat_Race_RaceId",
                table: "DeeStat",
                column: "RaceId",
                principalTable: "Race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeeStat_Stat_StatId",
                table: "DeeStat",
                column: "StatId",
                principalTable: "Stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
