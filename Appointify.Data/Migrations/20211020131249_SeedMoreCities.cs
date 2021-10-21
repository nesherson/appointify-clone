using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Appointify.Data.Migrations
{
    public partial class SeedMoreCities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryId", "CreatedAt", "ModifiedAt", "Name", "PostalCode" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2021, 10, 19, 10, 23, 0, 0, DateTimeKind.Unspecified), null, "Sarajevo", "71000" },
                    { 3, 1, new DateTime(2021, 10, 19, 10, 23, 0, 0, DateTimeKind.Unspecified), null, "Zenica", "72000" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
