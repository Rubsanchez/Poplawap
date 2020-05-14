using Microsoft.EntityFrameworkCore.Migrations;

namespace Poplawap.Backend.Migrations
{
    public partial class Updatestatusmodel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Statuses");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Statuses",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Statuses");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Statuses",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }
    }
}
