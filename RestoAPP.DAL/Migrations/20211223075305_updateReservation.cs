using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoAPP.DAL.Migrations
{
    public partial class updateReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Horaire",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ServiceReservation",
                table: "Reservation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Horaire",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceReservation",
                table: "Reservation",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
