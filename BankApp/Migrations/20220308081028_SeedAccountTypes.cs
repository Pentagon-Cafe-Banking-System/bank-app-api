using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class SeedAccountTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45729abd-61e4-4843-96ba-4e31fa790cef");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cf65d6a7-70fa-48f0-ba2c-31c2ea2d6c67");

            migrationBuilder.AlterColumn<decimal>(
                name: "InterestRate",
                table: "AccountTypes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Code", "InterestRate", "Name" },
                values: new object[,]
                {
                    { (short)1, "CA", 0.5m, "Current Account" },
                    { (short)2, "SA", 3m, "Savings Account" },
                    { (short)3, "FCA", 0m, "Foreign Currency Account" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "AccountTypes",
                keyColumn: "Id",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "807d5f4e-351f-4b54-8242-be76da85bada");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2528658-279a-474e-a0c6-bfb6cdde25b2");

            migrationBuilder.AlterColumn<float>(
                name: "InterestRate",
                table: "AccountTypes",
                type: "real",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2640a0-0496-4010-bc27-424e0e5c6f78",
                column: "ConcurrencyStamp",
                value: "1e9d42cc-87b6-4357-a4b0-e2c64bdde014");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45729abd-61e4-4843-96ba-4e31fa790cef", "b526fd67-6782-49df-9018-a25c7fb151da", "Customer", "CUSTOMER" },
                    { "cf65d6a7-70fa-48f0-ba2c-31c2ea2d6c67", "09122fde-d315-48ae-95c6-2f18e7d244fc", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a4165b4-0aca-43fb-a390-294781ee377f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cc16f24a-a280-406c-b83e-b6290865ca21", "AQAAAAEAACcQAAAAEIf9ZgZOH8qGSa8YKMAxu4+R/xNnY5eYxZES0FW29+AWmIQO+V+/Hu+tehQ70okk7w==", "dad330da-8536-490b-a3fd-6542086f1bf2" });
        }
    }
}
