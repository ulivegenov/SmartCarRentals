using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class DriverImageUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Drivers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Drivers");
        }
    }
}
