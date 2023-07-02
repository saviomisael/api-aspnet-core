using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesWebApi.Migrations
{
    public partial class NtoNRelationships2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform");

            migrationBuilder.AlterColumn<string>(
                name: "PlatformId",
                table: "GamePlatform",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GamePlatform",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)");

            migrationBuilder.AlterColumn<string>(
                name: "GenreId",
                table: "GameGenre",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GameGenre",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "PlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform");

            migrationBuilder.AlterColumn<string>(
                name: "PlatformId",
                table: "GamePlatform",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GamePlatform",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GenreId",
                table: "GameGenre",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GameGenre",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
