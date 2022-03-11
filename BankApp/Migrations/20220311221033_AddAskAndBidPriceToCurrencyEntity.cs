using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class AddAskAndBidPriceToCurrencyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "807d5f4e-351f-4b54-8242-be76da85bada");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2528658-279a-474e-a0c6-bfb6cdde25b2");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Currencies");

            migrationBuilder.AddColumn<decimal>(
                name: "Ask",
                table: "Currencies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Bid",
                table: "Currencies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2640a0-0496-4010-bc27-424e0e5c6f78",
                column: "ConcurrencyStamp",
                value: "72aa59b3-c16c-4133-a64e-edcc21a98726");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b2812331-4f64-4d5a-a514-e80fdf9d36fe", "6f3448a5-ff69-45d1-9109-5f77aa424595", "Customer", "CUSTOMER" },
                    { "bbfb7fb8-dfdc-4e63-9b2e-30e5881a45ff", "343ff2ba-9ea1-4143-af1b-39700f1a5c23", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a4165b4-0aca-43fb-a390-294781ee377f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a284636-725c-437b-8b68-09942d807f4e", "AQAAAAEAACcQAAAAEHS2im4HJ1tHTJD32VI6S48EggVrB03r4ZZgsdFypIL3zW6KYRRCNZ+0a/jxNIwD4A==", "6af7bd42-bef9-4824-8977-6f31596e5170" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2812331-4f64-4d5a-a514-e80fdf9d36fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbfb7fb8-dfdc-4e63-9b2e-30e5881a45ff");

            migrationBuilder.DropColumn(
                name: "Ask",
                table: "Currencies");

            migrationBuilder.DropColumn(
                name: "Bid",
                table: "Currencies");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Currencies",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2640a0-0496-4010-bc27-424e0e5c6f78",
                column: "ConcurrencyStamp",
                value: "abb5e447-55e5-4933-b506-399d463c01da");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "807d5f4e-351f-4b54-8242-be76da85bada", "b0bbbf50-832d-4224-94db-c1530d2a461c", "Customer", "CUSTOMER" },
                    { "d2528658-279a-474e-a0c6-bfb6cdde25b2", "70a09a01-4b4e-454b-a485-a8fa80480bdc", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a4165b4-0aca-43fb-a390-294781ee377f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0419b62c-a5cf-468c-9d5b-82e4cf96acde", "AQAAAAEAACcQAAAAEJFcQMZqGdwspCjETa30C+lAU35QU9Ca7XvQO+BVQ1nTHoccibMmN1+AbagFtEKJ+g==", "2fdd70ff-76e2-4a8f-bdbd-5a6e6520ef6e" });
        }
    }
}
