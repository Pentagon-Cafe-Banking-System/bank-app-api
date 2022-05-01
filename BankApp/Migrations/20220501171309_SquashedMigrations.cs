using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankApp.Migrations
{
    public partial class SquashedMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    InterestRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Bid = table.Column<decimal>(type: "numeric", nullable: false),
                    Ask = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admins_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    NationalId = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CityOfBirth = table.Column<string>(type: "text", nullable: false),
                    FathersName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Salary = table.Column<double>(type: "double precision", nullable: false),
                    Gender = table.Column<char>(type: "character(1)", nullable: false),
                    DateOfEmployment = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByIp = table.Column<string>(type: "text", nullable: true),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RevokedByIp = table.Column<string>(type: "text", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "text", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "text", nullable: true),
                    AppUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TransactionLimit = table.Column<decimal>(type: "numeric", nullable: false),
                    ValidThru = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Cvv = table.Column<string>(type: "text", nullable: false),
                    Pin = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CardTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_CardTypes_CardTypeId",
                        column: x => x.CardTypeId,
                        principalTable: "CardTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTypeCurrencies",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypeCurrencies", x => new { x.AccountTypeId, x.CurrencyId });
                    table.ForeignKey(
                        name: "FK_AccountTypeCurrencies_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountTypeCurrencies_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    TransferLimit = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AccountTypeId = table.Column<int>(type: "integer", nullable: false),
                    CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_AccountTypes_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    City = table.Column<string>(type: "text", nullable: false),
                    PostCode = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CardType = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    ReceiverAccountNumber = table.Column<string>(type: "text", nullable: false),
                    ReceiverName = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Ordered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Executed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReasonFailed = table.Column<string>(type: "text", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsFailed = table.Column<bool>(type: "boolean", nullable: false),
                    SenderAccountId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_SenderAccountId",
                        column: x => x.SenderAccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "Id", "Code", "InterestRate", "Name" },
                values: new object[,]
                {
                    { 1, "CA", 0.5m, "Current Account" },
                    { 2, "SA", 3m, "Savings Account" },
                    { 3, "FCA", 0m, "Foreign Currency Account" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d174a543-f853-43b7-9d13-2ef0a492fd50", "5ba565f6-2c9a-4233-aeac-ce7c8b74b1a6", "Customer", "CUSTOMER" },
                    { "d1b8fe3b-26fb-4c0a-850a-6854833be5d2", "a185570a-d8a9-42d6-ae2d-a1fedb963d07", "Employee", "EMPLOYEE" },
                    { "fa2640a0-0496-4010-bc27-424e0e5c6f78", "ab4672cc-d659-40ad-9cb0-08ab02d2a6d8", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "LockoutEnabled", "LockoutEnd", "NormalizedUserName", "PasswordHash", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7a4165b4-0aca-43fb-a390-294781ee377f", 0, "981d4b1f-7fcc-430a-9b4b-c439bbae5b21", false, null, "ADMIN", "AQAAAAEAACcQAAAAENXmsqKmDPDcLRamrDZ1b6auHAQBzUpysleeoeoUcdm9LSgx2RrroDJUr6JdgFSX4Q==", "76e259a2-f256-4d16-bd3d-9c66646b68c3", false, "admin" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "AD", "Andorra" },
                    { 2, "AE", "United Arab Emirates" },
                    { 3, "AF", "Afghanistan" },
                    { 4, "AG", "Antigua and Barbuda" },
                    { 5, "AI", "Anguilla" },
                    { 6, "AL", "Albania" },
                    { 7, "AM", "Armenia" },
                    { 8, "AN", "Netherlands Antilles" },
                    { 9, "AO", "Angola" },
                    { 10, "AQ", "Antarctica" },
                    { 11, "AR", "Argentina" },
                    { 12, "AS", "American Samoa" },
                    { 13, "AT", "Austria" },
                    { 14, "AU", "Australia" },
                    { 15, "AW", "Aruba" },
                    { 16, "AZ", "Azerbaijan" },
                    { 17, "BA", "Bosnia and Herzegovina" },
                    { 18, "BB", "Barbados" },
                    { 19, "BD", "Bangladesh" },
                    { 20, "BE", "Belgium" },
                    { 21, "BF", "Burkina Faso" },
                    { 22, "BG", "Bulgaria" },
                    { 23, "BH", "Bahrain" },
                    { 24, "BI", "Burundi" },
                    { 25, "BJ", "Benin" },
                    { 26, "BM", "Bermuda" },
                    { 27, "BN", "Brunei" },
                    { 28, "BO", "Bolivia" },
                    { 29, "BR", "Brazil" },
                    { 30, "BS", "Bahamas" },
                    { 31, "BT", "Bhutan" },
                    { 32, "BV", "Bouvet Island" },
                    { 33, "BW", "Botswana" },
                    { 34, "BY", "Belarus" },
                    { 35, "BZ", "Belize" },
                    { 36, "CA", "Canada" },
                    { 37, "CC", "Cocos [Keeling] Islands" },
                    { 38, "CD", "Congo [DRC]" },
                    { 39, "CF", "Central African Republic" },
                    { 40, "CG", "Congo [Republic]" },
                    { 41, "CH", "Switzerland" },
                    { 42, "CI", "Côte d'Ivoire" },
                    { 43, "CK", "Cook Islands" },
                    { 44, "CL", "Chile" },
                    { 45, "CM", "Cameroon" },
                    { 46, "CN", "China" },
                    { 47, "CO", "Colombia" },
                    { 48, "CR", "Costa Rica" },
                    { 49, "CU", "Cuba" },
                    { 50, "CV", "Cape Verde" },
                    { 51, "CX", "Christmas Island" },
                    { 52, "CY", "Cyprus" },
                    { 53, "CZ", "Czech Republic" },
                    { 54, "DE", "Germany" },
                    { 55, "DJ", "Djibouti" },
                    { 56, "DK", "Denmark" },
                    { 57, "DM", "Dominica" },
                    { 58, "DO", "Dominican Republic" },
                    { 59, "DZ", "Algeria" },
                    { 60, "EC", "Ecuador" },
                    { 61, "EE", "Estonia" },
                    { 62, "EG", "Egypt" },
                    { 63, "EH", "Western Sahara" },
                    { 64, "ER", "Eritrea" },
                    { 65, "ES", "Spain" },
                    { 66, "ET", "Ethiopia" },
                    { 67, "FI", "Finland" },
                    { 68, "FJ", "Fiji" },
                    { 69, "FK", "Falkland Islands [Islas Malvinas]" },
                    { 70, "FM", "Micronesia" },
                    { 71, "FO", "Faroe Islands" },
                    { 72, "FR", "France" },
                    { 73, "GA", "Gabon" },
                    { 74, "GB", "United Kingdom" },
                    { 75, "GD", "Grenada" },
                    { 76, "GE", "Georgia" },
                    { 77, "GF", "French Guiana" },
                    { 78, "GG", "Guernsey" },
                    { 79, "GH", "Ghana" },
                    { 80, "GI", "Gibraltar" },
                    { 81, "GL", "Greenland" },
                    { 82, "GM", "Gambia" },
                    { 83, "GN", "Guinea" },
                    { 84, "GP", "Guadeloupe" },
                    { 85, "GQ", "Equatorial Guinea" },
                    { 86, "GR", "Greece" },
                    { 87, "GS", "South Georgia and the South Sandwich Islands" },
                    { 88, "GT", "Guatemala" },
                    { 89, "GU", "Guam" },
                    { 90, "GW", "Guinea-Bissau" },
                    { 91, "GY", "Guyana" },
                    { 92, "GZ", "Gaza Strip" },
                    { 93, "HK", "Hong Kong" },
                    { 94, "HM", "Heard Island and McDonald Islands" },
                    { 95, "HN", "Honduras" },
                    { 96, "HR", "Croatia" },
                    { 97, "HT", "Haiti" },
                    { 98, "HU", "Hungary" },
                    { 99, "ID", "Indonesia" },
                    { 100, "IE", "Ireland" },
                    { 101, "IL", "Israel" },
                    { 102, "IM", "Isle of Man" },
                    { 103, "IN", "India" },
                    { 104, "IO", "British Indian Ocean Territory" },
                    { 105, "IQ", "Iraq" },
                    { 106, "IR", "Iran" },
                    { 107, "IS", "Iceland" },
                    { 108, "IT", "Italy" },
                    { 109, "JE", "Jersey" },
                    { 110, "JM", "Jamaica" },
                    { 111, "JO", "Jordan" },
                    { 112, "JP", "Japan" },
                    { 113, "KE", "Kenya" },
                    { 114, "KG", "Kyrgyzstan" },
                    { 115, "KH", "Cambodia" },
                    { 116, "KI", "Kiribati" },
                    { 117, "KM", "Comoros" },
                    { 118, "KN", "Saint Kitts and Nevis" },
                    { 119, "KP", "North Korea" },
                    { 120, "KR", "South Korea" },
                    { 121, "KW", "Kuwait" },
                    { 122, "KY", "Cayman Islands" },
                    { 123, "KZ", "Kazakhstan" },
                    { 124, "LA", "Laos" },
                    { 125, "LB", "Lebanon" },
                    { 126, "LC", "Saint Lucia" },
                    { 127, "LI", "Liechtenstein" },
                    { 128, "LK", "Sri Lanka" },
                    { 129, "LR", "Liberia" },
                    { 130, "LS", "Lesotho" },
                    { 131, "LT", "Lithuania" },
                    { 132, "LU", "Luxembourg" },
                    { 133, "LV", "Latvia" },
                    { 134, "LY", "Libya" },
                    { 135, "MA", "Morocco" },
                    { 136, "MC", "Monaco" },
                    { 137, "MD", "Moldova" },
                    { 138, "ME", "Montenegro" },
                    { 139, "MG", "Madagascar" },
                    { 140, "MH", "Marshall Islands" },
                    { 141, "MK", "Macedonia [FYROM]" },
                    { 142, "ML", "Mali" },
                    { 143, "MM", "Myanmar [Burma]" },
                    { 144, "MN", "Mongolia" },
                    { 145, "MO", "Macau" },
                    { 146, "MP", "Northern Mariana Islands" },
                    { 147, "MQ", "Martinique" },
                    { 148, "MR", "Mauritania" },
                    { 149, "MS", "Montserrat" },
                    { 150, "MT", "Malta" },
                    { 151, "MU", "Mauritius" },
                    { 152, "MV", "Maldives" },
                    { 153, "MW", "Malawi" },
                    { 154, "MX", "Mexico" },
                    { 155, "MY", "Malaysia" },
                    { 156, "MZ", "Mozambique" },
                    { 157, "NA", "Namibia" },
                    { 158, "NC", "New Caledonia" },
                    { 159, "NE", "Niger" },
                    { 160, "NF", "Norfolk Island" },
                    { 161, "NG", "Nigeria" },
                    { 162, "NI", "Nicaragua" },
                    { 163, "NL", "Netherlands" },
                    { 164, "NO", "Norway" },
                    { 165, "NP", "Nepal" },
                    { 166, "NR", "Nauru" },
                    { 167, "NU", "Niue" },
                    { 168, "NZ", "New Zealand" },
                    { 169, "OM", "Oman" },
                    { 170, "PA", "Panama" },
                    { 171, "PE", "Peru" },
                    { 172, "PF", "French Polynesia" },
                    { 173, "PG", "Papua New Guinea" },
                    { 174, "PH", "Philippines" },
                    { 175, "PK", "Pakistan" },
                    { 176, "PL", "Poland" },
                    { 177, "PM", "Saint Pierre and Miquelon" },
                    { 178, "PN", "Pitcairn Islands" },
                    { 179, "PR", "Puerto Rico" },
                    { 180, "PS", "Palestinian Territories" },
                    { 181, "PT", "Portugal" },
                    { 182, "PW", "Palau" },
                    { 183, "PY", "Paraguay" },
                    { 184, "QA", "Qatar" },
                    { 185, "RE", "Réunion" },
                    { 186, "RO", "Romania" },
                    { 187, "RS", "Serbia" },
                    { 188, "RU", "Russia" },
                    { 189, "RW", "Rwanda" },
                    { 190, "SA", "Saudi Arabia" },
                    { 191, "SB", "Solomon Islands" },
                    { 192, "SC", "Seychelles" },
                    { 193, "SD", "Sudan" },
                    { 194, "SE", "Sweden" },
                    { 195, "SG", "Singapore" },
                    { 196, "SH", "Saint Helena" },
                    { 197, "SI", "Slovenia" },
                    { 198, "SJ", "Svalbard and Jan Mayen" },
                    { 199, "SK", "Slovakia" },
                    { 200, "SL", "Sierra Leone" },
                    { 201, "SM", "San Marino" },
                    { 202, "SN", "Senegal" },
                    { 203, "SO", "Somalia" },
                    { 204, "SR", "Suriname" },
                    { 205, "ST", "São Tomé and Príncipe" },
                    { 206, "SV", "El Salvador" },
                    { 207, "SY", "Syria" },
                    { 208, "SZ", "Swaziland" },
                    { 209, "TC", "Turks and Caicos Islands" },
                    { 210, "TD", "Chad" },
                    { 211, "TF", "French Southern Territories" },
                    { 212, "TG", "Togo" },
                    { 213, "TH", "Thailand" },
                    { 214, "TJ", "Tajikistan" },
                    { 215, "TK", "Tokelau" },
                    { 216, "TL", "Timor-Leste" },
                    { 217, "TM", "Turkmenistan" },
                    { 218, "TN", "Tunisia" },
                    { 219, "TO", "Tonga" },
                    { 220, "TR", "Turkey" },
                    { 221, "TT", "Trinidad and Tobago" },
                    { 222, "TV", "Tuvalu" },
                    { 223, "TW", "Taiwan" },
                    { 224, "TZ", "Tanzania" },
                    { 225, "UA", "Ukraine" },
                    { 226, "UG", "Uganda" },
                    { 227, "UM", "U.S. Minor Outlying Islands" },
                    { 228, "US", "United States" },
                    { 229, "UY", "Uruguay" },
                    { 230, "UZ", "Uzbekistan" },
                    { 231, "VA", "Vatican City" },
                    { 232, "VC", "Saint Vincent and the Grenadines" },
                    { 233, "VE", "Venezuela" },
                    { 234, "VG", "British Virgin Islands" },
                    { 235, "VI", "U.S. Virgin Islands" },
                    { 236, "VN", "Vietnam" },
                    { 237, "VU", "Vanuatu" },
                    { 238, "WF", "Wallis and Futuna" },
                    { 239, "WS", "Samoa" },
                    { 240, "XK", "Kosovo" },
                    { 241, "YE", "Yemen" },
                    { 242, "YT", "Mayotte" },
                    { 243, "ZA", "South Africa" },
                    { 244, "ZM", "Zambia" },
                    { 245, "ZW", "Zimbabwe" }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Ask", "Bid", "Code" },
                values: new object[,]
                {
                    { 1, 0m, 0m, "PLN" },
                    { 2, 0m, 0m, "USD" },
                    { 3, 0m, 0m, "AUD" },
                    { 4, 0m, 0m, "CAD" },
                    { 5, 0m, 0m, "EUR" },
                    { 6, 0m, 0m, "HUF" },
                    { 7, 0m, 0m, "CHF" },
                    { 8, 0m, 0m, "GBP" },
                    { 9, 0m, 0m, "JPY" },
                    { 10, 0m, 0m, "CZK" },
                    { 11, 0m, 0m, "DKK" },
                    { 12, 0m, 0m, "NOK" },
                    { 13, 0m, 0m, "SEK" },
                    { 14, 0m, 0m, "XDR" }
                });

            migrationBuilder.InsertData(
                table: "AccountTypeCurrencies",
                columns: new[] { "AccountTypeId", "CurrencyId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 3, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 3, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 3, 14 }
                });

            migrationBuilder.InsertData(
                table: "Admins",
                column: "Id",
                value: "7a4165b4-0aca-43fb-a390-294781ee377f");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "fa2640a0-0496-4010-bc27-424e0e5c6f78", "7a4165b4-0aca-43fb-a390-294781ee377f" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountTypeId",
                table: "Accounts",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CurrencyId",
                table: "Accounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Number",
                table: "Accounts",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypeCurrencies_CurrencyId",
                table: "AccountTypeCurrencies",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTypes_Code",
                table: "AccountTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CountryId",
                table: "Addresses",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerId",
                table: "Addresses",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardOrders_CustomerId",
                table: "CardOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_CardTypeId",
                table: "Cards",
                column: "CardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Number",
                table: "Cards",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTypes_Code",
                table: "CardTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Countries_Code",
                table: "Countries",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_SenderAccountId",
                table: "Transfers",
                column: "SenderAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTypeCurrencies");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "CardOrders");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
