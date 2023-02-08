using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeoHelper.Data.Migrations
{
    public partial class Added_result_save : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                table: "Requests");
        }
    }
}
