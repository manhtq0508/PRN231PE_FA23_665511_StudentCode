using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusinessObject.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BranchAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromCountry = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SilverJewelries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetalWeight = table.Column<float>(type: "real", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductionYear = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SilverJewelries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SilverJewelries_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BranchAccounts",
                columns: new[] { "Id", "Email", "FullName", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "Admin", "admin", 1 },
                    { 2, "member@gmail.com", "Member", "member", 2 },
                    { 3, "manager@gmail.com", "Manager", "manager", 3 },
                    { 4, "staff@gmail.com", "Staff", "staff", 4 }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "FromCountry", "Name" },
                values: new object[,]
                {
                    { 1, "Description 1", "USA", "Rings" },
                    { 2, "Description 2", "Vietnam", "Necklaces" },
                    { 3, "Description 3", "Japan", "Bracelets" },
                    { 4, "Description 4", "China", "Earrings" }
                });

            migrationBuilder.InsertData(
                table: "SilverJewelries",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "MetalWeight", "Name", "Price", "ProductionYear" },
                values: new object[,]
                {
                    { 1, 1, new DateOnly(1999, 1, 1), "Description 1", 0.1f, "Silver Jewelry 1", 50.0m, 1999 },
                    { 2, 2, new DateOnly(2000, 2, 1), "Description 2", 1.1f, "Silver Jewelry 2", 10.0m, 2000 },
                    { 3, 3, new DateOnly(2021, 12, 12), "Description 3", 2.1f, "Silver Jewelry 3", 24.5m, 2021 },
                    { 4, 4, new DateOnly(1997, 1, 1), "Description 4", 3.1f, "Silver Jewelry 4", 54.0m, 1997 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SilverJewelries_CategoryId",
                table: "SilverJewelries",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BranchAccounts");

            migrationBuilder.DropTable(
                name: "SilverJewelries");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
