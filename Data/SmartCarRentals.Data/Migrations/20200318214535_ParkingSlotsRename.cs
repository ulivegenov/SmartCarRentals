using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class ParkingSlotsRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLots_Cars_CarId",
                table: "ParkingLots");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingLots_Parkings_ParkingId",
                table: "ParkingLots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingLots",
                table: "ParkingLots");

            migrationBuilder.RenameTable(
                name: "ParkingLots",
                newName: "ParkingSlots");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingLots_ParkingId",
                table: "ParkingSlots",
                newName: "IX_ParkingSlots_ParkingId");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingLots_IsDeleted",
                table: "ParkingSlots",
                newName: "IX_ParkingSlots_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingLots_CarId",
                table: "ParkingSlots",
                newName: "IX_ParkingSlots_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingSlots",
                table: "ParkingSlots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlots_Cars_CarId",
                table: "ParkingSlots",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlots_Parkings_ParkingId",
                table: "ParkingSlots",
                column: "ParkingId",
                principalTable: "Parkings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlots_Cars_CarId",
                table: "ParkingSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlots_Parkings_ParkingId",
                table: "ParkingSlots");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ParkingSlots",
                table: "ParkingSlots");

            migrationBuilder.RenameTable(
                name: "ParkingSlots",
                newName: "ParkingLots");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSlots_ParkingId",
                table: "ParkingLots",
                newName: "IX_ParkingLots_ParkingId");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSlots_IsDeleted",
                table: "ParkingLots",
                newName: "IX_ParkingLots_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_ParkingSlots_CarId",
                table: "ParkingLots",
                newName: "IX_ParkingLots_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ParkingLots",
                table: "ParkingLots",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLots_Cars_CarId",
                table: "ParkingLots",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingLots_Parkings_ParkingId",
                table: "ParkingLots",
                column: "ParkingId",
                principalTable: "Parkings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
