using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesWebApi.Migrations
{
    public partial class NtoNRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "GamesFK",
                table: "GamePlatform");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Games_GamesId",
                table: "GamePlatform",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Games_GamesId",
                table: "GamePlatform");

            migrationBuilder.AddForeignKey(
                name: "GamesFK",
                table: "GamePlatform",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
