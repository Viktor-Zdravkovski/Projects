using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental.DataBase.Migrations
{
    public partial class MovieRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<int>(type: "int", nullable: false),
                    Language = table.Column<int>(type: "int", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Length = table.Column<TimeSpan>(type: "time", nullable: false),
                    AgeRestriction = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsSubscriptionExpired = table.Column<bool>(type: "bit", nullable: false),
                    SubscriptionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "AgeRestriction", "Genre", "IsAvailable", "Language", "Length", "Quantity", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, 15, 0, true, 1, new TimeSpan(0, 2, 0, 0, 0), 10, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Action Movie 1" },
                    { 2, 10, 1, true, 2, new TimeSpan(0, 1, 45, 0, 0), 5, new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Comedy Movie 1" },
                    { 3, 12, 4, false, 3, new TimeSpan(0, 2, 30, 0, 0), 8, new DateTime(2022, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sci-Fi Movie 1" },
                    { 4, 15, 2, true, 4, new TimeSpan(0, 2, 12, 0, 0), 3, new DateTime(2019, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Parasite" },
                    { 5, 15, 0, true, 1, new TimeSpan(0, 2, 16, 0, 0), 10, new DateTime(1999, 3, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Matrix" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Age", "CardNumber", "CreatedOn", "FullName", "IsSubscriptionExpired", "SubscriptionType" },
                values: new object[,]
                {
                    { 1, 28, "1234-5678-9012-3456", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice Johnson", false, 1 },
                    { 2, 34, "2345-6789-0123-4567", new DateTime(2022, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob Smith", true, 0 },
                    { 3, 45, "3456-7890-1234-5678", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlie Brown", false, 0 },
                    { 4, 29, "4567-8901-2345-6789", new DateTime(2023, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diana Prince", false, 0 },
                    { 5, 38, "5678-9012-3456-7890", new DateTime(2021, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Edward Nygma", true, 1 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movie");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
