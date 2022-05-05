using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeuBanku.Migrations
{
    public partial class InitialIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("2286804c-4edd-4d6f-92dc-59d2e4d64ca6"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("321bf96e-46e0-4e8c-8072-5b5a5ac6e69a"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("448b387b-569f-49f0-86fb-3cc1b4ed326c"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("4bbc4d9a-f327-4436-8cc1-719f872597c3"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("4e1ba097-adc4-48db-85df-64c6ec223156"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("626ee7b6-022c-4be2-88f9-6602fb379297"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("6c7c8175-c6d3-4384-b918-c4c709243d5c"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("7ba25c43-2382-4f90-87d8-1cd584402db9"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("894cffea-010e-4e6d-a791-ad6b982f5c97"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("9131ea56-de7b-42be-a66d-fa556497324b"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("9be3439e-d0b9-4e6d-9b2a-b3740238a1c0"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("9de4c104-714c-4bc7-ac15-b14053a0d6ff"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("cadba806-c34a-4507-8665-46440940d73c"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("dfd70383-7f85-4456-8106-8ee4f6c8aa68"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("e6631f43-6266-43ea-b87b-058e0a249fa6"));

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: new Guid("e7b48021-e481-4ceb-9b1d-0b23417787b8"));

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

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
    }
}
