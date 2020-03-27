using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class ImagePropertyRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Cars",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Drivers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Cars",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
