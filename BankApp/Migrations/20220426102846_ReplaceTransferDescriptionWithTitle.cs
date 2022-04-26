using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class ReplaceTransferDescriptionWithTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "192afec1-f785-4e38-85c1-b0ec98dab755");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f26e797-34b0-4623-9947-d4715a521df2");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Transfers",
                newName: "Title");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "Title",
                table: "Transfers",
                newName: "Description");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa2640a0-0496-4010-bc27-424e0e5c6f78",
                column: "ConcurrencyStamp",
                value: "34c5caaf-9389-4399-b6af-041cc670525b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "192afec1-f785-4e38-85c1-b0ec98dab755", "1588b7ff-0725-4abd-9019-afa4b226c055", "Employee", "EMPLOYEE" },
                    { "8f26e797-34b0-4623-9947-d4715a521df2", "cbee7785-b947-4f2f-82d2-446f0aa060cc", "Customer", "CUSTOMER" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7a4165b4-0aca-43fb-a390-294781ee377f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9c928691-9e51-4040-a921-db02a30884c8", "AQAAAAEAACcQAAAAEBGREII4XYQTxapgFsMuziwW2L70y811rJFlls9iU8iWuwWqzKmUcWeZVyQVKLGg5Q==", "f659070a-5755-4936-bddd-edeb4f720a8f" });
        }
    }
}
