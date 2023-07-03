using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesWebApi.Migrations
{
    public partial class RemoveInsertectionEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Games_GamesId",
                table: "GameGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenres_Genres_GenresId",
                table: "GameGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatforms_Games_GamesId",
                table: "GamePlatforms");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatforms_Platforms_PlatformsId",
                table: "GamePlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlatforms",
                table: "GamePlatforms");

            migrationBuilder.DropIndex(
                name: "IX_GamePlatforms_GamesId",
                table: "GamePlatforms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenres",
                table: "GameGenres");

            migrationBuilder.DropIndex(
                name: "IX_GameGenres_GamesId",
                table: "GameGenres");

            migrationBuilder.RenameTable(
                name: "GamePlatforms",
                newName: "GamePlatform");

            migrationBuilder.RenameTable(
                name: "GameGenres",
                newName: "GameGenre");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatforms_PlatformsId",
                table: "GamePlatform",
                newName: "IX_GamePlatform_PlatformsId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenres_GenresId",
                table: "GameGenre",
                newName: "IX_GameGenre_GenresId");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GamePlatform",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "GamePlatform",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GameGenre",
                type: "nvarchar(36)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "GenreId",
                table: "GameGenre",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlatform",
                table: "GamePlatform",
                columns: new[] { "GamesId", "PlatformsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenre",
                table: "GameGenre",
                columns: new[] { "GamesId", "GenresId" });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_GameId",
                table: "GamePlatform",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatform_PlatformId",
                table: "GamePlatform",
                column: "PlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GameId",
                table: "GameGenre",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Games_GamesId",
                table: "GameGenre",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenre_Genres_GenresId",
                table: "GameGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Games_GamesId",
                table: "GamePlatform",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform",
                column: "PlatformId",
                principalTable: "Platforms",
                principalColumn: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformsId",
                table: "GamePlatform",
                column: "PlatformsId",
                principalTable: "Platforms",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GameId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Games_GamesId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenreId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GameGenre_Genres_GenresId",
                table: "GameGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Games_GameId",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Games_GamesId",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformId",
                table: "GamePlatform");

            migrationBuilder.DropForeignKey(
                name: "FK_GamePlatform_Platforms_PlatformsId",
                table: "GamePlatform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GamePlatform",
                table: "GamePlatform");

            migrationBuilder.DropIndex(
                name: "IX_GamePlatform_GameId",
                table: "GamePlatform");

            migrationBuilder.DropIndex(
                name: "IX_GamePlatform_PlatformId",
                table: "GamePlatform");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GameGenre",
                table: "GameGenre");

            migrationBuilder.DropIndex(
                name: "IX_GameGenre_GameId",
                table: "GameGenre");

            migrationBuilder.DropIndex(
                name: "IX_GameGenre_GenreId",
                table: "GameGenre");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "GamePlatform");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "GameGenre");

            migrationBuilder.RenameTable(
                name: "GamePlatform",
                newName: "GamePlatforms");

            migrationBuilder.RenameTable(
                name: "GameGenre",
                newName: "GameGenres");

            migrationBuilder.RenameIndex(
                name: "IX_GamePlatform_PlatformsId",
                table: "GamePlatforms",
                newName: "IX_GamePlatforms_PlatformsId");

            migrationBuilder.RenameIndex(
                name: "IX_GameGenre_GenresId",
                table: "GameGenres",
                newName: "IX_GameGenres_GenresId");

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GamePlatforms",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GameId",
                table: "GameGenres",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GamePlatforms",
                table: "GamePlatforms",
                columns: new[] { "GameId", "PlatformsId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_GameGenres",
                table: "GameGenres",
                columns: new[] { "GameId", "GenresId" });

            migrationBuilder.CreateIndex(
                name: "IX_GamePlatforms_GamesId",
                table: "GamePlatforms",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_GameGenres_GamesId",
                table: "GameGenres",
                column: "GamesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Games_GamesId",
                table: "GameGenres",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameGenres_Genres_GenresId",
                table: "GameGenres",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatforms_Games_GamesId",
                table: "GamePlatforms",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "GameId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePlatforms_Platforms_PlatformsId",
                table: "GamePlatforms",
                column: "PlatformsId",
                principalTable: "Platforms",
                principalColumn: "PlatformId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
