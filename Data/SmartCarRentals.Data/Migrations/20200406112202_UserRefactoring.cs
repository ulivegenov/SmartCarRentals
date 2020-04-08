using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class UserRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ApplicationUserId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_AspNetUsers_ApplicationUserId",
                table: "Cars",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
