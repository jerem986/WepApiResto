using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL.Views
{
    public class VBooking //class de la vue
    {
        public DateTime DateDeRes { get; set; }
        public int IsNoon { get; set; }
        public int Total { get; set; }
    }
}

//création de la classe qui va être générée par la vue
//ajout du DbSet dans le resto db context public DbSet<VBooking> VBooking { get; set; } 
//ajout dans l'override en dessous de mb.Entity<VBooking>().ToView("VBooking") pour savoir sur quel modèle se baser
//création d'une migration vide via un add-migration dans la console du package manager console
//on enregistre dans un string la requete SQL
// et on l'applique via migrationBuilder.Sql(QueryView)
//on applique cela via un update-database dans la console

//Demo a https://bitbucket.org/KhunLy/labotb/src/master/
