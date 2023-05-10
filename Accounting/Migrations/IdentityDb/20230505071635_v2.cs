using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85417699-9823-4742-a807-62995241df0a", null, "AQAAAAIAAYagAAAAENFW1TvNk+PghLmuyQjmr8Ech2UZSFM8hIoyFBb/efC+Op1W8aej8edVpzYCdD5kZw==", "0075b2a3-4169-4364-ba41-69357689c0c0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "272b719a-dc7c-403d-9f33-8ea1ff989f24");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3025da50-aab4-4206-90ce-167f82c9d4ea", "admin@gmail.com", "AQAAAAEAACcQAAAAENrJv8egTDTdcrPngHNYwKHti9qtZG3bWGpx+A8qcY0xxW+E2kb5udClOHKi8YWFGQ==", "f5bf3739-c4a3-46a7-a94e-6a40d78ad9af" });
        }
    }
}
