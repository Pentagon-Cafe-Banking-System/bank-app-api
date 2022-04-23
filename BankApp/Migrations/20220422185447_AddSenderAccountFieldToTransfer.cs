using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankApp.Migrations
{
    public partial class AddSenderAccountFieldToTransfer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_AccountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_AccountId",
                table: "Transfers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2812331-4f64-4d5a-a514-e80fdf9d36fe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbfb7fb8-dfdc-4e63-9b2e-30e5881a45ff");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Transfers");

            migrationBuilder.AddColumn<long>(
                name: "SenderAccountId",
                table: "Transfers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SenderAccountId",
                table: "Transfers",
                column: "SenderAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_SenderAccountId",
                table: "Transfers",
                column: "SenderAccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_Accounts_SenderAccountId",
                table: "Transfers");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_SenderAccountId",
                table: "Transfers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "192afec1-f785-4e38-85c1-b0ec98dab755");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8f26e797-34b0-4623-9947-d4715a521df2");

            migrationBuilder.DropColumn(
                name: "SenderAccountId",
                table: "Transfers");

            migrationBuilder.AddColumn<long>(
                name: "AccountId",
                table: "Transfers",
                type: "bigint",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountId",
                table: "Transfers",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_Accounts_AccountId",
                table: "Transfers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
