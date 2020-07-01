using Microsoft.EntityFrameworkCore.Migrations;

namespace Dao.Migrations
{
    public partial class typofix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcionType",
                table: "UserActivites");

            migrationBuilder.AddColumn<string>(
                name: "ActionType",
                table: "UserActivites",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionType",
                table: "UserActivites");

            migrationBuilder.AddColumn<string>(
                name: "AcionType",
                table: "UserActivites",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
