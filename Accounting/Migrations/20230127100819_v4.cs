using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "MeatSalePrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "MeatEntryPrices",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeatPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeatId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    ActiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeatPrices", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeatPrices");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "MeatSalePrices");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "MeatEntryPrices");
        }
    }
}
