using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class ChangeSecondNameToMiddleNameInCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "317c761f-ccab-426a-ba39-e451d5467a41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5542293-4ffd-47e0-becd-a5fe5870d961");

            migrationBuilder.RenameColumn(
                name: "SecondName",
                table: "Customers",
                newName: "MiddleName");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2640a0-0496-4010-bc27-424e0e5c6f78",
                column: "ConcurrencyStamp",
                value: "9ae22fdd-0f88-4d65-8ced-fe9886d32d82");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7fc593ff-a977-498a-b8be-2b6ec64f36fb", "c20b2650-ecf6-43f8-a63e-8d3da63e4b79", "Customer", "CUSTOMER" },
                    { "a2abddb1-8856-4223-ba41-b8c611dcb3d2", "ac32d118-3330-42ca-b0d6-1935ccc380a6", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a4165b4-0aca-43fb-a390-294781ee377f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33955989-e010-4521-b1b4-de0108a80515", "AQAAAAEAACcQAAAAEPQAoRa7JRjQcLu09Y3PxUJxcZ5FS4aE3rNnSqE2hSe1O2v1g1HlKszNoROWiCcuNg==", "5e348759-48bd-42bf-8035-d09a3577cb3a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fc593ff-a977-498a-b8be-2b6ec64f36fb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2abddb1-8856-4223-ba41-b8c611dcb3d2");

            migrationBuilder.RenameColumn(
                name: "MiddleName",
                table: "Customers",
                newName: "SecondName");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2640a0-0496-4010-bc27-424e0e5c6f78",
                column: "ConcurrencyStamp",
                value: "db495135-4ef7-4569-8234-3d41c74c93a6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "317c761f-ccab-426a-ba39-e451d5467a41", "8013224b-065b-43a6-8e7d-4bb8ab69ab28", "Customer", "CUSTOMER" },
                    { "b5542293-4ffd-47e0-becd-a5fe5870d961", "58b58848-acf0-4aeb-8ab2-80d8569e3d5f", "Employee", "EMPLOYEE" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a4165b4-0aca-43fb-a390-294781ee377f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2fd26ab2-c0d6-49a8-b67c-5c1b7b06a07d", "AQAAAAEAACcQAAAAEJ0Lbf2osOdxlQJMsZzyc4OzwcCydzonAAAQ1yLuHiSXzx5IoS/yMp285Oq9TYzZZg==", "fa43a81c-032a-4197-8949-0fafb43bf946" });
        }
    }
}
