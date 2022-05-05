using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeuBanku.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AccountType = table.Column<int>(type: "int", nullable: false),
                    PeriodToTakeMoneyInYears = table.Column<int>(type: "int", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    OpenedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Extracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OutcomingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutAccountNumber = table.Column<int>(type: "int", nullable: false),
                    InAccountNumber = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "AccountNumber", "Cost", "CreatedDate", "IsDisabled", "ServiceName" },
                values: new object[,]
                {
                    { new Guid("2286804c-4edd-4d6f-92dc-59d2e4d64ca6"), 87249661, 608m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1143), false, "ZAP Satelite" },
                    { new Guid("321bf96e-46e0-4e8c-8072-5b5a5ac6e69a"), 86003242, 9621m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1138), false, "TV Cabo" },
                    { new Guid("448b387b-569f-49f0-86fb-3cc1b4ed326c"), 84171016, 9437m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1124), false, "Hospital" },
                    { new Guid("4bbc4d9a-f327-4436-8cc1-719f872597c3"), 78188786, 2266m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1096), false, "Unitel" },
                    { new Guid("4e1ba097-adc4-48db-85df-64c6ec223156"), 60544708, 2597m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1128), false, "Special Service" },
                    { new Guid("626ee7b6-022c-4be2-88f9-6602fb379297"), 98222814, 1427m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1145), false, "ZAP Fibra" },
                    { new Guid("6c7c8175-c6d3-4384-b918-c4c709243d5c"), 28345451, 1373m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1137), false, "DSTV" },
                    { new Guid("7ba25c43-2382-4f90-87d8-1cd584402db9"), 85025770, 170m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1110), false, "Africell" },
                    { new Guid("894cffea-010e-4e6d-a791-ad6b982f5c97"), 27416049, 5418m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1104), false, "Movicel" },
                    { new Guid("9131ea56-de7b-42be-a66d-fa556497324b"), 48991143, 745m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1106), false, "EDEL" },
                    { new Guid("9be3439e-d0b9-4e6d-9b2a-b3740238a1c0"), 6641069, 8188m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1126), false, "Governament" },
                    { new Guid("9de4c104-714c-4bc7-ac15-b14053a0d6ff"), 11145231, 1954m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1135), false, "Angola Telecom" },
                    { new Guid("cadba806-c34a-4507-8665-46440940d73c"), 5509365, 2718m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1147), false, "NetOne" },
                    { new Guid("dfd70383-7f85-4456-8106-8ee4f6c8aa68"), 53977192, 113m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1133), false, "Other Service" },
                    { new Guid("e6631f43-6266-43ea-b87b-058e0a249fa6"), 96693791, 780m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1108), false, "EPAL" },
                    { new Guid("e7b48021-e481-4ceb-9b1d-0b23417787b8"), 69527067, 5362m, new DateTime(2022, 4, 21, 19, 37, 32, 786, DateTimeKind.Utc).AddTicks(1130), false, "School" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Extracts");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
