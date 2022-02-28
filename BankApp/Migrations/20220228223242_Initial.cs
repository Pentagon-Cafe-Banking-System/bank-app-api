using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BankApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    InterestRate = table.Column<float>(type: "real", nullable: false)
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
                    Id = table.Column<short>(type: "smallint", nullable: false)
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
                    Id = table.Column<short>(type: "smallint", nullable: false)
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
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
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
                    SecondName = table.Column<string>(type: "text", nullable: false),
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
                    CardTypeId = table.Column<short>(type: "smallint", nullable: false)
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
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<string>(type: "text", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    TransferLimit = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    AccountTypeId = table.Column<short>(type: "smallint", nullable: false),
                    CurrencyId = table.Column<short>(type: "smallint", nullable: false),
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
                    CountryId = table.Column<short>(type: "smallint", nullable: false)
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
                    Description = table.Column<string>(type: "text", nullable: false),
                    Ordered = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Executed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReasonFailed = table.Column<string>(type: "text", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsFailed = table.Column<bool>(type: "boolean", nullable: false),
                    AccountId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45729abd-61e4-4843-96ba-4e31fa790cef", "b526fd67-6782-49df-9018-a25c7fb151da", "Customer", "CUSTOMER" },
                    { "cf65d6a7-70fa-48f0-ba2c-31c2ea2d6c67", "09122fde-d315-48ae-95c6-2f18e7d244fc", "Employee", "EMPLOYEE" },
                    { "fa2640a0-0496-4010-bc27-424e0e5c6f78", "1e9d42cc-87b6-4357-a4b0-e2c64bdde014", "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "LockoutEnabled", "LockoutEnd", "NormalizedUserName", "PasswordHash", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7a4165b4-0aca-43fb-a390-294781ee377f", 0, "cc16f24a-a280-406c-b83e-b6290865ca21", false, null, "ADMIN", "AQAAAAEAACcQAAAAEIf9ZgZOH8qGSa8YKMAxu4+R/xNnY5eYxZES0FW29+AWmIQO+V+/Hu+tehQ70okk7w==", "dad330da-8536-490b-a3fd-6542086f1bf2", false, "admin" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, "AD", "Andorra" },
                    { (short)2, "AE", "United Arab Emirates" },
                    { (short)3, "AF", "Afghanistan" },
                    { (short)4, "AG", "Antigua and Barbuda" },
                    { (short)5, "AI", "Anguilla" },
                    { (short)6, "AL", "Albania" },
                    { (short)7, "AM", "Armenia" },
                    { (short)8, "AN", "Netherlands Antilles" },
                    { (short)9, "AO", "Angola" },
                    { (short)10, "AQ", "Antarctica" },
                    { (short)11, "AR", "Argentina" },
                    { (short)12, "AS", "American Samoa" },
                    { (short)13, "AT", "Austria" },
                    { (short)14, "AU", "Australia" },
                    { (short)15, "AW", "Aruba" },
                    { (short)16, "AZ", "Azerbaijan" },
                    { (short)17, "BA", "Bosnia and Herzegovina" },
                    { (short)18, "BB", "Barbados" },
                    { (short)19, "BD", "Bangladesh" },
                    { (short)20, "BE", "Belgium" },
                    { (short)21, "BF", "Burkina Faso" },
                    { (short)22, "BG", "Bulgaria" },
                    { (short)23, "BH", "Bahrain" },
                    { (short)24, "BI", "Burundi" },
                    { (short)25, "BJ", "Benin" },
                    { (short)26, "BM", "Bermuda" },
                    { (short)27, "BN", "Brunei" },
                    { (short)28, "BO", "Bolivia" },
                    { (short)29, "BR", "Brazil" },
                    { (short)30, "BS", "Bahamas" },
                    { (short)31, "BT", "Bhutan" },
                    { (short)32, "BV", "Bouvet Island" },
                    { (short)33, "BW", "Botswana" },
                    { (short)34, "BY", "Belarus" },
                    { (short)35, "BZ", "Belize" },
                    { (short)36, "CA", "Canada" },
                    { (short)37, "CC", "Cocos [Keeling] Islands" },
                    { (short)38, "CD", "Congo [DRC]" },
                    { (short)39, "CF", "Central African Republic" },
                    { (short)40, "CG", "Congo [Republic]" },
                    { (short)41, "CH", "Switzerland" },
                    { (short)42, "CI", "Côte d'Ivoire" },
                    { (short)43, "CK", "Cook Islands" },
                    { (short)44, "CL", "Chile" },
                    { (short)45, "CM", "Cameroon" },
                    { (short)46, "CN", "China" },
                    { (short)47, "CO", "Colombia" },
                    { (short)48, "CR", "Costa Rica" },
                    { (short)49, "CU", "Cuba" },
                    { (short)50, "CV", "Cape Verde" },
                    { (short)51, "CX", "Christmas Island" },
                    { (short)52, "CY", "Cyprus" },
                    { (short)53, "CZ", "Czech Republic" },
                    { (short)54, "DE", "Germany" },
                    { (short)55, "DJ", "Djibouti" },
                    { (short)56, "DK", "Denmark" },
                    { (short)57, "DM", "Dominica" },
                    { (short)58, "DO", "Dominican Republic" },
                    { (short)59, "DZ", "Algeria" },
                    { (short)60, "EC", "Ecuador" },
                    { (short)61, "EE", "Estonia" },
                    { (short)62, "EG", "Egypt" },
                    { (short)63, "EH", "Western Sahara" },
                    { (short)64, "ER", "Eritrea" },
                    { (short)65, "ES", "Spain" },
                    { (short)66, "ET", "Ethiopia" },
                    { (short)67, "FI", "Finland" },
                    { (short)68, "FJ", "Fiji" },
                    { (short)69, "FK", "Falkland Islands [Islas Malvinas]" },
                    { (short)70, "FM", "Micronesia" },
                    { (short)71, "FO", "Faroe Islands" },
                    { (short)72, "FR", "France" },
                    { (short)73, "GA", "Gabon" },
                    { (short)74, "GB", "United Kingdom" },
                    { (short)75, "GD", "Grenada" },
                    { (short)76, "GE", "Georgia" },
                    { (short)77, "GF", "French Guiana" },
                    { (short)78, "GG", "Guernsey" },
                    { (short)79, "GH", "Ghana" },
                    { (short)80, "GI", "Gibraltar" },
                    { (short)81, "GL", "Greenland" },
                    { (short)82, "GM", "Gambia" },
                    { (short)83, "GN", "Guinea" },
                    { (short)84, "GP", "Guadeloupe" },
                    { (short)85, "GQ", "Equatorial Guinea" },
                    { (short)86, "GR", "Greece" },
                    { (short)87, "GS", "South Georgia and the South Sandwich Islands" },
                    { (short)88, "GT", "Guatemala" },
                    { (short)89, "GU", "Guam" },
                    { (short)90, "GW", "Guinea-Bissau" },
                    { (short)91, "GY", "Guyana" },
                    { (short)92, "GZ", "Gaza Strip" },
                    { (short)93, "HK", "Hong Kong" },
                    { (short)94, "HM", "Heard Island and McDonald Islands" },
                    { (short)95, "HN", "Honduras" },
                    { (short)96, "HR", "Croatia" },
                    { (short)97, "HT", "Haiti" },
                    { (short)98, "HU", "Hungary" },
                    { (short)99, "ID", "Indonesia" },
                    { (short)100, "IE", "Ireland" },
                    { (short)101, "IL", "Israel" },
                    { (short)102, "IM", "Isle of Man" },
                    { (short)103, "IN", "India" },
                    { (short)104, "IO", "British Indian Ocean Territory" },
                    { (short)105, "IQ", "Iraq" },
                    { (short)106, "IR", "Iran" },
                    { (short)107, "IS", "Iceland" },
                    { (short)108, "IT", "Italy" },
                    { (short)109, "JE", "Jersey" },
                    { (short)110, "JM", "Jamaica" },
                    { (short)111, "JO", "Jordan" },
                    { (short)112, "JP", "Japan" },
                    { (short)113, "KE", "Kenya" },
                    { (short)114, "KG", "Kyrgyzstan" },
                    { (short)115, "KH", "Cambodia" },
                    { (short)116, "KI", "Kiribati" },
                    { (short)117, "KM", "Comoros" },
                    { (short)118, "KN", "Saint Kitts and Nevis" },
                    { (short)119, "KP", "North Korea" },
                    { (short)120, "KR", "South Korea" },
                    { (short)121, "KW", "Kuwait" },
                    { (short)122, "KY", "Cayman Islands" },
                    { (short)123, "KZ", "Kazakhstan" },
                    { (short)124, "LA", "Laos" },
                    { (short)125, "LB", "Lebanon" },
                    { (short)126, "LC", "Saint Lucia" },
                    { (short)127, "LI", "Liechtenstein" },
                    { (short)128, "LK", "Sri Lanka" },
                    { (short)129, "LR", "Liberia" },
                    { (short)130, "LS", "Lesotho" },
                    { (short)131, "LT", "Lithuania" },
                    { (short)132, "LU", "Luxembourg" },
                    { (short)133, "LV", "Latvia" },
                    { (short)134, "LY", "Libya" },
                    { (short)135, "MA", "Morocco" },
                    { (short)136, "MC", "Monaco" },
                    { (short)137, "MD", "Moldova" },
                    { (short)138, "ME", "Montenegro" },
                    { (short)139, "MG", "Madagascar" },
                    { (short)140, "MH", "Marshall Islands" },
                    { (short)141, "MK", "Macedonia [FYROM]" },
                    { (short)142, "ML", "Mali" },
                    { (short)143, "MM", "Myanmar [Burma]" },
                    { (short)144, "MN", "Mongolia" },
                    { (short)145, "MO", "Macau" },
                    { (short)146, "MP", "Northern Mariana Islands" },
                    { (short)147, "MQ", "Martinique" },
                    { (short)148, "MR", "Mauritania" },
                    { (short)149, "MS", "Montserrat" },
                    { (short)150, "MT", "Malta" },
                    { (short)151, "MU", "Mauritius" },
                    { (short)152, "MV", "Maldives" },
                    { (short)153, "MW", "Malawi" },
                    { (short)154, "MX", "Mexico" },
                    { (short)155, "MY", "Malaysia" },
                    { (short)156, "MZ", "Mozambique" },
                    { (short)157, "NA", "Namibia" },
                    { (short)158, "NC", "New Caledonia" },
                    { (short)159, "NE", "Niger" },
                    { (short)160, "NF", "Norfolk Island" },
                    { (short)161, "NG", "Nigeria" },
                    { (short)162, "NI", "Nicaragua" },
                    { (short)163, "NL", "Netherlands" },
                    { (short)164, "NO", "Norway" },
                    { (short)165, "NP", "Nepal" },
                    { (short)166, "NR", "Nauru" },
                    { (short)167, "NU", "Niue" },
                    { (short)168, "NZ", "New Zealand" },
                    { (short)169, "OM", "Oman" },
                    { (short)170, "PA", "Panama" },
                    { (short)171, "PE", "Peru" },
                    { (short)172, "PF", "French Polynesia" },
                    { (short)173, "PG", "Papua New Guinea" },
                    { (short)174, "PH", "Philippines" },
                    { (short)175, "PK", "Pakistan" },
                    { (short)176, "PL", "Poland" },
                    { (short)177, "PM", "Saint Pierre and Miquelon" },
                    { (short)178, "PN", "Pitcairn Islands" },
                    { (short)179, "PR", "Puerto Rico" },
                    { (short)180, "PS", "Palestinian Territories" },
                    { (short)181, "PT", "Portugal" },
                    { (short)182, "PW", "Palau" },
                    { (short)183, "PY", "Paraguay" },
                    { (short)184, "QA", "Qatar" },
                    { (short)185, "RE", "Réunion" },
                    { (short)186, "RO", "Romania" },
                    { (short)187, "RS", "Serbia" },
                    { (short)188, "RU", "Russia" },
                    { (short)189, "RW", "Rwanda" },
                    { (short)190, "SA", "Saudi Arabia" },
                    { (short)191, "SB", "Solomon Islands" },
                    { (short)192, "SC", "Seychelles" },
                    { (short)193, "SD", "Sudan" },
                    { (short)194, "SE", "Sweden" },
                    { (short)195, "SG", "Singapore" },
                    { (short)196, "SH", "Saint Helena" },
                    { (short)197, "SI", "Slovenia" },
                    { (short)198, "SJ", "Svalbard and Jan Mayen" },
                    { (short)199, "SK", "Slovakia" },
                    { (short)200, "SL", "Sierra Leone" },
                    { (short)201, "SM", "San Marino" },
                    { (short)202, "SN", "Senegal" },
                    { (short)203, "SO", "Somalia" },
                    { (short)204, "SR", "Suriname" },
                    { (short)205, "ST", "São Tomé and Príncipe" },
                    { (short)206, "SV", "El Salvador" },
                    { (short)207, "SY", "Syria" },
                    { (short)208, "SZ", "Swaziland" },
                    { (short)209, "TC", "Turks and Caicos Islands" },
                    { (short)210, "TD", "Chad" },
                    { (short)211, "TF", "French Southern Territories" },
                    { (short)212, "TG", "Togo" },
                    { (short)213, "TH", "Thailand" },
                    { (short)214, "TJ", "Tajikistan" },
                    { (short)215, "TK", "Tokelau" },
                    { (short)216, "TL", "Timor-Leste" },
                    { (short)217, "TM", "Turkmenistan" },
                    { (short)218, "TN", "Tunisia" },
                    { (short)219, "TO", "Tonga" },
                    { (short)220, "TR", "Turkey" },
                    { (short)221, "TT", "Trinidad and Tobago" },
                    { (short)222, "TV", "Tuvalu" },
                    { (short)223, "TW", "Taiwan" },
                    { (short)224, "TZ", "Tanzania" },
                    { (short)225, "UA", "Ukraine" },
                    { (short)226, "UG", "Uganda" },
                    { (short)227, "UM", "U.S. Minor Outlying Islands" },
                    { (short)228, "US", "United States" },
                    { (short)229, "UY", "Uruguay" },
                    { (short)230, "UZ", "Uzbekistan" },
                    { (short)231, "VA", "Vatican City" },
                    { (short)232, "VC", "Saint Vincent and the Grenadines" },
                    { (short)233, "VE", "Venezuela" },
                    { (short)234, "VG", "British Virgin Islands" },
                    { (short)235, "VI", "U.S. Virgin Islands" },
                    { (short)236, "VN", "Vietnam" },
                    { (short)237, "VU", "Vanuatu" },
                    { (short)238, "WF", "Wallis and Futuna" },
                    { (short)239, "WS", "Samoa" },
                    { (short)240, "XK", "Kosovo" },
                    { (short)241, "YE", "Yemen" },
                    { (short)242, "YT", "Mayotte" },
                    { (short)243, "ZA", "South Africa" },
                    { (short)244, "ZM", "Zambia" },
                    { (short)245, "ZW", "Zimbabwe" }
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
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_AccountId",
                table: "Transfers",
                column: "AccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
