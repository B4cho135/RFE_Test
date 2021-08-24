using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Migrations
{
    public partial class RemoveResultfromdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Comparisons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Comparisons",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
