using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoAPP.DAL.Migrations
{
    public partial class RetraitEmplacement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Emplacement_IdEmplacement",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "Emplacement");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_IdEmplacement",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "IdEmplacement",
                table: "Reservation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDeRes",
                table: "Reservation",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "HeureReservation",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeureReservation",
                table: "Reservation");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateDeRes",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AddColumn<int>(
                name: "IdEmplacement",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Emplacement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplacement", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdEmplacement",
                table: "Reservation",
                column: "IdEmplacement");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Emplacement_IdEmplacement",
                table: "Reservation",
                column: "IdEmplacement",
                principalTable: "Emplacement",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
