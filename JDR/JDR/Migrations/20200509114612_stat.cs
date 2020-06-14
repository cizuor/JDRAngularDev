using Microsoft.EntityFrameworkCore.Migrations;

namespace JDR.Migrations
{
    public partial class stat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(nullable: true),
                    Definition = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Stats = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatUtil",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ForStatId = table.Column<int>(nullable: true),
                    StatUtileId = table.Column<int>(nullable: true),
                    Valeur = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatUtil", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatUtil_Stat_ForStatId",
                        column: x => x.ForStatId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatUtil_Stat_StatUtileId",
                        column: x => x.StatUtileId,
                        principalTable: "Stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatUtil_ForStatId",
                table: "StatUtil",
                column: "ForStatId");

            migrationBuilder.CreateIndex(
                name: "IX_StatUtil_StatUtileId",
                table: "StatUtil",
                column: "StatUtileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatUtil");

            migrationBuilder.DropTable(
                name: "Stat");
        }
    }
}
