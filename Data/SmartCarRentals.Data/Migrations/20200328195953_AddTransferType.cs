using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartCarRentals.Data.Migrations
{
    public partial class AddTransferType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KmRun",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "TransferTypeId",
                table: "Transfers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TransfersTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransfersTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_TransferTypeId",
                table: "Transfers",
                column: "TransferTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransfersTypes_IsDeleted",
                table: "TransfersTypes",
                column: "IsDeleted");

            migrationBuilder.AddForeignKey(
                name: "FK_Transfers_TransfersTypes_TransferTypeId",
                table: "Transfers",
                column: "TransferTypeId",
                principalTable: "TransfersTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transfers_TransfersTypes_TransferTypeId",
                table: "Transfers");

            migrationBuilder.DropTable(
                name: "TransfersTypes");

            migrationBuilder.DropIndex(
                name: "IX_Transfers_TransferTypeId",
                table: "Transfers");

            migrationBuilder.DropColumn(
                name: "TransferTypeId",
                table: "Transfers");

            migrationBuilder.AddColumn<int>(
                name: "KmRun",
                table: "Transfers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
