using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental.DataBase.Migrations
{
    public partial class updatedNamesOfFilms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "DeadPool & Wolverine");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Mother of the bride");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Planet of the Apes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 1,
                column: "Title",
                value: "Action Movie 1");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 2,
                column: "Title",
                value: "Comedy Movie 1");

            migrationBuilder.UpdateData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: 3,
                column: "Title",
                value: "Sci-Fi Movie 1");
        }
    }
}
