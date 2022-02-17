using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class AddRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1dd560ba-d5cf-4e3a-89bc-b39bc84abfb7", "3c1f918b-d765-414f-976c-e0019c4e83e9", "Admin", "ADMIN" },
                    { "af8a48bb-b7aa-42c1-bc3e-f7d3544109f6", "e4675f81-e4c8-48bd-8d87-6c6fc788e275", "Employee", "EMPLOYEE" },
                    { "b3404aa9-4387-4f21-a033-96160fec068a", "f9fabc74-76cd-498e-a0db-17a206aef3db", "Customer", "CUSTOMER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dd560ba-d5cf-4e3a-89bc-b39bc84abfb7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af8a48bb-b7aa-42c1-bc3e-f7d3544109f6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b3404aa9-4387-4f21-a033-96160fec068a");
        }
    }
}
