using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProjectContemporaryProgramming.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TheGabeTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(nullable: true),
                    Birthday = table.Column<string>(nullable: true),
                    CollegeProgram = table.Column<string>(nullable: true),
                    YearInProgram = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheGabeTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheJohnsTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FavoriteSport = table.Column<string>(nullable: true),
                    FavoriteVideoGame = table.Column<string>(nullable: true),
                    FavoriteTVShow = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheJohnsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheMattsTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FavoriteBreakfast = table.Column<string>(nullable: true),
                    FavoriteDinner = table.Column<string>(nullable: true),
                    FavoriteDessert = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheMattsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TheNicksTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FavoriteTa = table.Column<string>(nullable: true),
                    FavoriteTeacher = table.Column<string>(nullable: true),
                    FavoriteClass = table.Column<string>(nullable: true),
                    IAmALiar = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheNicksTable", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TheGabeTable",
                columns: new[] { "Id", "Birthday", "CollegeProgram", "FullName", "YearInProgram" },
                values: new object[] { 1, "Jan 1, 1991", "Information Technology", "Gabe Newell", "3" });

            migrationBuilder.InsertData(
                table: "TheJohnsTable",
                columns: new[] { "Id", "FavoriteSport", "FavoriteTVShow", "FavoriteVideoGame", "FirstName", "LastName" },
                values: new object[] { 1, "Handegg", "The Wire", "Knights of the Old Republic II", "John", "Doe" });

            migrationBuilder.InsertData(
                table: "TheMattsTable",
                columns: new[] { "Id", "FavoriteBreakfast", "FavoriteDessert", "FavoriteDinner", "FirstName", "LastName" },
                values: new object[] { 1, "Eggs and Ham", "Dessert Eggs", "Ham", "Matt", "Caudill" });

            migrationBuilder.InsertData(
                table: "TheNicksTable",
                columns: new[] { "Id", "FavoriteClass", "FavoriteTa", "FavoriteTeacher", "FirstName", "IAmALiar", "LastName" },
                values: new object[] { 1, "3", "exampleTA", "Information Technology", "Gabe", 1, "Newell" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TheGabeTable");

            migrationBuilder.DropTable(
                name: "TheJohnsTable");

            migrationBuilder.DropTable(
                name: "TheMattsTable");

            migrationBuilder.DropTable(
                name: "TheNicksTable");
        }
    }
}
