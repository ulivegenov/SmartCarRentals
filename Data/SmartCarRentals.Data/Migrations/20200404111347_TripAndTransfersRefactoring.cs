using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class TripAndTransfersRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasPaid",
                table: "Trips",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasVote",
                table: "Trips",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasPaid",
                table: "Transfers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasVote",
                table: "Transfers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasPaid",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "HasVote",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "HasPaid",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "HasVote",
                table: "Transfers");
        }
    }
}
