using Microsoft.EntityFrameworkCore.Migrations;

namespace JDR.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
  

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Perso",
                nullable: true);


            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudo = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Perso_UserId",
                table: "Perso",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Perso_Users_UserId",
                table: "Perso",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Perso_Users_UserId",
                table: "Perso");

            migrationBuilder.DropForeignKey(
                name: "FK_StatUtil_Stat_ForStatId",
                table: "StatUtil");

            migrationBuilder.DropForeignKey(
                name: "FK_StatUtil_Stat_StatUtileId",
                table: "StatUtil");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatUtil",
                table: "StatUtil");

            migrationBuilder.DropIndex(
                name: "IX_StatUtil_StatUtileId",
                table: "StatUtil");

            migrationBuilder.DropIndex(
                name: "IX_Perso_UserId",
                table: "Perso");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "StatUtil");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Perso");

            migrationBuilder.AlterColumn<int>(
                name: "StatUtileId",
                table: "StatUtil",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ForStatId",
                table: "StatUtil",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatUtil",
                table: "StatUtil",
                columns: new[] { "StatUtileId", "ForStatId" });

            migrationBuilder.AddForeignKey(
                name: "FK_StatUtil_Stat_ForStatId",
                table: "StatUtil",
                column: "ForStatId",
                principalTable: "Stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StatUtil_Stat_StatUtileId",
                table: "StatUtil",
                column: "StatUtileId",
                principalTable: "Stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
