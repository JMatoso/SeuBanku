using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeuBanku.Migrations
{
    public partial class ExtractFromAndTo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ServiceName",
                table: "Extracts",
                newName: "To");

            migrationBuilder.AddColumn<string>(
                name: "From",
                table: "Extracts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "From",
                table: "Extracts");

            migrationBuilder.RenameColumn(
                name: "To",
                table: "Extracts",
                newName: "ServiceName");
        }
    }
}
