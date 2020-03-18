using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class FreeParkingSlotsRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeParkingLots",
                table: "Parkings");

            migrationBuilder.AddColumn<int>(
                name: "FreeParkingSlots",
                table: "Parkings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FreeParkingSlots",
                table: "Parkings");

            migrationBuilder.AddColumn<int>(
                name: "FreeParkingLots",
                table: "Parkings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
