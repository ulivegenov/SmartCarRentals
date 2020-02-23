using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class OneToOneUserDriverLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DriverLicenseId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "DriverLicenses",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DriverLicenseId",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DriverLicenseId",
                table: "AspNetUsers",
                column: "DriverLicenseId",
                unique: true,
                filter: "[DriverLicenseId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DriverLicenseId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "DriverLicenses");

            migrationBuilder.AlterColumn<string>(
                name: "DriverLicenseId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DriverLicenseId",
                table: "AspNetUsers",
                column: "DriverLicenseId");
        }
    }
}
