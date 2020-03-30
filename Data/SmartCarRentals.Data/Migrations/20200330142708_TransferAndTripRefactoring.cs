using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class TransferAndTripRefactoring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DriversRatings_TransferId",
                table: "DriversRatings");

            migrationBuilder.DropIndex(
                name: "IX_CarsRatings_TripId",
                table: "CarsRatings");

            migrationBuilder.DropColumn(
                name: "CarRatingId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DriverRatingId",
                table: "Transfers");

            migrationBuilder.CreateIndex(
                name: "IX_DriversRatings_TransferId",
                table: "DriversRatings",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_CarsRatings_TripId",
                table: "CarsRatings",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DriversRatings_TransferId",
                table: "DriversRatings");

            migrationBuilder.DropIndex(
                name: "IX_CarsRatings_TripId",
                table: "CarsRatings");

            migrationBuilder.AddColumn<int>(
                name: "CarRatingId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DriverRatingId",
                table: "Transfers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DriversRatings_TransferId",
                table: "DriversRatings",
                column: "TransferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarsRatings_TripId",
                table: "CarsRatings",
                column: "TripId",
                unique: true);
        }
    }
}
