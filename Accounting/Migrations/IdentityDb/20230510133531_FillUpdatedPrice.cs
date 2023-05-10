using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class FillUpdatedPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UpdatedPriceDate" },
                values: new object[] { "d6457dab-8f0a-41e0-8205-f6dc78c3d1a3", "AQAAAAIAAYagAAAAEFeKzU0LtwKHngu3QcyxPDUVcGIYJMvHKZpDZdmq32bE6eV0ZH043OeZIghMfBU0pw==", "0711e091-e34f-4cba-bdc3-2792737666f1", new DateTime(2023, 5, 10, 13, 35, 31, 492, DateTimeKind.Utc).AddTicks(4902) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UpdatedPriceDate" },
                values: new object[] { "21f8f2ff-b2ae-41ee-bb0a-9a8c1219463d", "AQAAAAIAAYagAAAAEJxPGfgcIYNe3KRaMZxrWGRupwuYtOu8iy0e2fEMZR5HNtMQGlysZ6HIcNrmu9s4dA==", "44caa0ea-2905-45a6-84ac-6a84feabf344", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
