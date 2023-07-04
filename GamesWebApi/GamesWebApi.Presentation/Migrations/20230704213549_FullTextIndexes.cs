using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesWebApi.Migrations
{
    public partial class FullTextIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE FULLTEXT CATALOG FTS_GName_GrName_PName", true);
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON dbo.Games(Name) KEY INDEX IX_Games_Name ON FTS_GName_GrName_PName WITH STOPLIST = OFF, CHANGE_TRACKING AUTO", true);
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON dbo.Genres(Name) KEY INDEX IX_Genres_Name ON FTS_GName_GrName_PName WITH STOPLIST = OFF, CHANGE_TRACKING AUTO", true);
            migrationBuilder.Sql("CREATE FULLTEXT INDEX ON dbo.Platforms(Name) KEY INDEX IX_Platforms_Name ON FTS_GName_GrName_PName WITH STOPLIST = OFF, CHANGE_TRACKING AUTO", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FULLTEXT INDEX ON dbo.Platforms", true);
            migrationBuilder.Sql("DROP FULLTEXT INDEX ON dbo.Genres", true);
            migrationBuilder.Sql("DROP FULLTEXT INDEX ON dbo.Games", true);
            migrationBuilder.Sql("DROP FULLTEXT CATALOG FTS_GName_GrName_PName", true);
        }
    }
}
