using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations
{
    /// <inheritdoc />
    public partial class v16 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LunarCreatedDate",
                table: "RecycleBins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarModifiedDate",
                table: "RecycleBins",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarActiveDate",
                table: "MeatPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarCreatedDate",
                table: "MeatPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarModifiedDate",
                table: "MeatPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarActiveDate",
                table: "MeatBillPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarCreatedDate",
                table: "MeatBillPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarModifiedDate",
                table: "MeatBillPrices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarCreatedDate",
                table: "Histories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarActiveDate",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarCreatedDate",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LunarModifiedDate",
                table: "Bills",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LunarCreatedDate",
                table: "RecycleBins");

            migrationBuilder.DropColumn(
                name: "LunarModifiedDate",
                table: "RecycleBins");

            migrationBuilder.DropColumn(
                name: "LunarActiveDate",
                table: "MeatPrices");

            migrationBuilder.DropColumn(
                name: "LunarCreatedDate",
                table: "MeatPrices");

            migrationBuilder.DropColumn(
                name: "LunarModifiedDate",
                table: "MeatPrices");

            migrationBuilder.DropColumn(
                name: "LunarActiveDate",
                table: "MeatBillPrices");

            migrationBuilder.DropColumn(
                name: "LunarCreatedDate",
                table: "MeatBillPrices");

            migrationBuilder.DropColumn(
                name: "LunarModifiedDate",
                table: "MeatBillPrices");

            migrationBuilder.DropColumn(
                name: "LunarCreatedDate",
                table: "Histories");

            migrationBuilder.DropColumn(
                name: "LunarActiveDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "LunarCreatedDate",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "LunarModifiedDate",
                table: "Bills");
        }
    }
}
