using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class TransferRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transfers");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Transfers",
                type: "decimal(18,4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
