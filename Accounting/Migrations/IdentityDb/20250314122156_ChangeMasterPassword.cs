using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accounting.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class ChangeMasterPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UpdatedPriceDate" },
                values: new object[] { "02eb84c1-78a8-4656-8b44-e4dfb771423c", "AQAAAAIAAYagAAAAEDxVAjV7ZIPpefx9cMHBTnPIvEao3MjW2F6w2+uKu9wphyKDpeJogMw4isEXCdXCPA==", "1bdf6737-41d3-4d1e-b82a-1bdc3af70721", new DateTime(2025, 3, 14, 12, 21, 56, 51, DateTimeKind.Utc).AddTicks(1650) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UpdatedPriceDate" },
                values: new object[] { "d6457dab-8f0a-41e0-8205-f6dc78c3d1a3", "AQAAAAIAAYagAAAAEFeKzU0LtwKHngu3QcyxPDUVcGIYJMvHKZpDZdmq32bE6eV0ZH043OeZIghMfBU0pw==", "0711e091-e34f-4cba-bdc3-2792737666f1", new DateTime(2023, 5, 10, 13, 35, 31, 492, DateTimeKind.Utc).AddTicks(4902) });
        }
    }
}
