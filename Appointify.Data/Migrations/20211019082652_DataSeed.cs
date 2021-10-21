using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointify.Data.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[] { 1, new DateTime(2021, 10, 19, 10, 23, 0, 0, DateTimeKind.Unspecified), null, "Bosna i Hercegovina" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedAt", "ModifiedAt", "Name", "PostalCode" },
                values: new object[] { 1, 1, new DateTime(2021, 10, 19, 10, 23, 0, 0, DateTimeKind.Unspecified), null, "Mostar", "88000" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CityId", "CreatedAt", "Email", "FirstName", "LastName", "ModifiedAt", "PasswordHash", "PasswordSalt", "Role", "Username" },
                values: new object[] { 1, 1, new DateTime(2021, 10, 19, 10, 23, 0, 0, DateTimeKind.Unspecified), "site.admin@breakpoint.ba", "Site", "Admin", null, "drjkcfZBOSHWFPKbZ14lsBOzgbGX4cgIK9bxWL+DmJc=", "uy00sIGnRe5kJLPJP7xxeQ==", 0, "site.admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
