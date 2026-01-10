using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagement.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class Adjusted_Names : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Method",
                table: "Payments",
                newName: "PaymentMethod");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentMethod",
                table: "Payments",
                newName: "Method");
        }
    }
}
