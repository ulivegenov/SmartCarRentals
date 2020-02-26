using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class OneToOneMappingRatings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarRating_Cars_CarId",
                table: "CarRating");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRating_AspNetUsers_ClientId",
                table: "CarRating");

            migrationBuilder.DropForeignKey(
                name: "FK_CarRating_Trips_TripId",
                table: "CarRating");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_AspNetUsers_ClientId",
                table: "DriverRating");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Drivers_DriverId",
                table: "DriverRating");

            migrationBuilder.DropForeignKey(
                name: "FK_DriverRating_Transfers_TransferId",
                table: "DriverRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverRating",
                table: "DriverRating");

            migrationBuilder.DropIndex(
                name: "IX_DriverRating_TransferId",
                table: "DriverRating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarRating",
                table: "CarRating");

            migrationBuilder.DropIndex(
                name: "IX_CarRating_TripId",
                table: "CarRating");

            migrationBuilder.RenameTable(
                name: "DriverRating",
                newName: "DriversRatings");

            migrationBuilder.RenameTable(
                name: "CarRating",
                newName: "CarsRatings");

            migrationBuilder.RenameIndex(
                name: "IX_DriverRating_IsDeleted",
                table: "DriversRatings",
                newName: "IX_DriversRatings_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_DriverRating_ClientId",
                table: "DriversRatings",
                newName: "IX_DriversRatings_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CarRating_IsDeleted",
                table: "CarsRatings",
                newName: "IX_CarsRatings_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_CarRating_ClientId",
                table: "CarsRatings",
                newName: "IX_CarsRatings_ClientId");

            migrationBuilder.AddColumn<int>(
                name: "CarRatingId",
                table: "Trips",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DriverRatingId",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriversRatings",
                table: "DriversRatings",
                columns: new[] { "DriverId", "ClientId", "TransferId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarsRatings",
                table: "CarsRatings",
                columns: new[] { "CarId", "ClientId", "TripId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_CarsRatings_Cars_CarId",
                table: "CarsRatings",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsRatings_AspNetUsers_ClientId",
                table: "CarsRatings",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarsRatings_Trips_TripId",
                table: "CarsRatings",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriversRatings_AspNetUsers_ClientId",
                table: "DriversRatings",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriversRatings_Drivers_DriverId",
                table: "DriversRatings",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriversRatings_Transfers_TransferId",
                table: "DriversRatings",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarsRatings_Cars_CarId",
                table: "CarsRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsRatings_AspNetUsers_ClientId",
                table: "CarsRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_CarsRatings_Trips_TripId",
                table: "CarsRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_DriversRatings_AspNetUsers_ClientId",
                table: "DriversRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_DriversRatings_Drivers_DriverId",
                table: "DriversRatings");

            migrationBuilder.DropForeignKey(
                name: "FK_DriversRatings_Transfers_TransferId",
                table: "DriversRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriversRatings",
                table: "DriversRatings");

            migrationBuilder.DropIndex(
                name: "IX_DriversRatings_TransferId",
                table: "DriversRatings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarsRatings",
                table: "CarsRatings");

            migrationBuilder.DropIndex(
                name: "IX_CarsRatings_TripId",
                table: "CarsRatings");

            migrationBuilder.DropColumn(
                name: "CarRatingId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "DriverRatingId",
                table: "Transfers");

            migrationBuilder.RenameTable(
                name: "DriversRatings",
                newName: "DriverRating");

            migrationBuilder.RenameTable(
                name: "CarsRatings",
                newName: "CarRating");

            migrationBuilder.RenameIndex(
                name: "IX_DriversRatings_IsDeleted",
                table: "DriverRating",
                newName: "IX_DriverRating_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_DriversRatings_ClientId",
                table: "DriverRating",
                newName: "IX_DriverRating_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_CarsRatings_IsDeleted",
                table: "CarRating",
                newName: "IX_CarRating_IsDeleted");

            migrationBuilder.RenameIndex(
                name: "IX_CarsRatings_ClientId",
                table: "CarRating",
                newName: "IX_CarRating_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverRating",
                table: "DriverRating",
                columns: new[] { "DriverId", "ClientId", "TransferId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarRating",
                table: "CarRating",
                columns: new[] { "CarId", "ClientId", "TripId" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverRating_TransferId",
                table: "DriverRating",
                column: "TransferId");

            migrationBuilder.CreateIndex(
                name: "IX_CarRating_TripId",
                table: "CarRating",
                column: "TripId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarRating_Cars_CarId",
                table: "CarRating",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRating_AspNetUsers_ClientId",
                table: "CarRating",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CarRating_Trips_TripId",
                table: "CarRating",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_AspNetUsers_ClientId",
                table: "DriverRating",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Drivers_DriverId",
                table: "DriverRating",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DriverRating_Transfers_TransferId",
                table: "DriverRating",
                column: "TransferId",
                principalTable: "Transfers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
