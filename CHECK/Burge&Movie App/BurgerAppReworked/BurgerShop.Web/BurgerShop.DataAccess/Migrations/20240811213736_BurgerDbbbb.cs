using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BurgerShop.DataBase.Migrations
{
    public partial class BurgerDbbbb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpensAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosesAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelivered = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Burgers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsVegan = table.Column<bool>(type: "bit", nullable: false),
                    IsVegetarian = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    HasFries = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Burgers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Burgers_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Address", "ClosesAt", "Name", "OpensAt" },
                values: new object[,]
                {
                    { 1, "123 Main St, Springfield", new DateTime(2024, 8, 11, 21, 0, 0, 0, DateTimeKind.Local), "Downtown Diner", new DateTime(2024, 8, 11, 9, 0, 0, 0, DateTimeKind.Local) },
                    { 2, "456 Ocean Blvd, Beach City", new DateTime(2024, 8, 11, 23, 0, 0, 0, DateTimeKind.Local), "Seaside Cafe", new DateTime(2024, 8, 11, 9, 0, 0, 0, DateTimeKind.Local) },
                    { 3, "789 Peak Rd, Alpine Town", new DateTime(2024, 8, 11, 23, 0, 0, 0, DateTimeKind.Local), "Mountain View Restaurant", new DateTime(2024, 8, 11, 8, 0, 0, 0, DateTimeKind.Local) },
                    { 4, "101 Skyline Dr, Metropolis", new DateTime(2024, 8, 11, 0, 0, 0, 0, DateTimeKind.Local), "City Lights Bistro", new DateTime(2024, 8, 11, 9, 0, 0, 0, DateTimeKind.Local) },
                    { 5, "202 Riverbank Ave, Water Town", new DateTime(2024, 8, 11, 23, 0, 0, 0, DateTimeKind.Local), "Riverside Grill", new DateTime(2024, 8, 11, 7, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Address", "FullName", "IsDelivered", "LocationId" },
                values: new object[,]
                {
                    { 1, "123 Main St, Springfield", "John Doe", true, 1 },
                    { 2, "456 Oak Ave, Metropolis", "Jane Smith", false, 2 },
                    { 3, "789 Pine Rd, Gotham", "Alice Johnson", true, 3 },
                    { 4, "101 Maple Dr, Star City", "Bob Brown", false, 4 },
                    { 5, "202 Elm St, Smallville", "Charlie Green", true, 5 }
                });

            migrationBuilder.InsertData(
                table: "Burgers",
                columns: new[] { "Id", "HasFries", "IsVegan", "IsVegetarian", "Name", "OrderId", "Price" },
                values: new object[,]
                {
                    { 1, true, false, false, "Classic Beef Burger", 1, 10 },
                    { 2, false, true, true, "Vegan Delight", 2, 12 },
                    { 3, true, false, false, "Chicken Burger", 3, 11 },
                    { 4, false, false, true, "Cheese Veggie Burger", 4, 9 },
                    { 5, true, true, true, "Spicy Black Bean Burger", 5, 8 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Burgers_OrderId",
                table: "Burgers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_LocationId",
                table: "Orders",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Burgers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
