using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoAPP.DAL.Migrations
{
    public partial class V22RemoveValidationsStatutsInEnumAndMore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_ValidationStatuts_IdValidationStatus",
                table: "Reservation");

            migrationBuilder.DropTable(
                name: "ValidationStatuts");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_IdValidationStatus",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "IdValidationStatus",
                table: "Reservation",
                newName: "ValidationStatuts");

            migrationBuilder.RenameColumn(
                name: "Horaire",
                table: "Reservation",
                newName: "ServiceReservation");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Repas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Repas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidationStatuts",
                table: "Reservation",
                newName: "IdValidationStatus");

            migrationBuilder.RenameColumn(
                name: "ServiceReservation",
                table: "Reservation",
                newName: "Horaire");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Repas",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Repas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ValidationStatuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValidationStatut = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationStatuts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdValidationStatus",
                table: "Reservation",
                column: "IdValidationStatus");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_ValidationStatuts_IdValidationStatus",
                table: "Reservation",
                column: "IdValidationStatus",
                principalTable: "ValidationStatuts",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
