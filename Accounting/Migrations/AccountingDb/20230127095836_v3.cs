using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillId",
                table: "MeatSalePrices");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "MeatEntryPrices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
