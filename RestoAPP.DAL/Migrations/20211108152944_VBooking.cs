using Microsoft.EntityFrameworkCore.Migrations;

namespace RestoAPP.DAL.Migrations
{
    public partial class VBooking : Migration //on crée une migration vide avec un simple add-migration dans la console
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string QueryView = @"CREATE VIEW VBooking 
                AS(
                SELECT r.DateDeRes, r.IsNoon, Sum(NbPers) as Total
                FROM 
                (SELECT 
	                *, 
	                case when HeureReservation < 5 then 1 else 0 end IsNoon
                FROM Reservation) r

                GROUP BY r.DateDeRes, r.IsNoon)"; //on enregistre la requete sql de génération de vue dans un string
            migrationBuilder.Sql(QueryView); //on l'ajoute a la migration et on fait l'update-database
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
