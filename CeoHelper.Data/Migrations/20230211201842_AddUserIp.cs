using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeoHelper.Data.Migrations
{
    public partial class AddUserIp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "AspNetUsers");
        }
    }
}
