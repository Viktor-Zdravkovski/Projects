using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental.DataBase.Migrations
{
    public partial class DbWithoutReturnedOn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnedOn",
                table: "Rental");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnedOn",
                table: "Rental",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rental",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReturnedOn",
                value: new DateTime(2024, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rental",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReturnedOn",
                value: new DateTime(2024, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rental",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReturnedOn",
                value: new DateTime(2024, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rental",
                keyColumn: "Id",
                keyValue: 4,
                column: "ReturnedOn",
                value: new DateTime(2024, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Rental",
                keyColumn: "Id",
                keyValue: 5,
                column: "ReturnedOn",
                value: new DateTime(2024, 7, 9, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
