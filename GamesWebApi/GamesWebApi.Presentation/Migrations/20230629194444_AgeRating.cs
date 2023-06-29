using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesWebApi.Migrations
{
    public partial class AgeRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeRatings",
                columns: table => new
                {
                    AgeRatingId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Age = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AgeRatingId", x => x.AgeRatingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgeRatings");
        }
    }
}
