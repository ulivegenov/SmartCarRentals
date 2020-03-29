using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class TransferAndDriverRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingLokation",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "StartingLocation",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "HireStatus",
                table: "Drivers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HireStatus",
                table: "Drivers");

            migrationBuilder.AddColumn<string>(
                name: "EndingLokation",
                table: "Transfers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartingLocation",
                table: "Transfers",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: false,
                defaultValue: "");
        }
    }
}
