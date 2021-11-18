using Microsoft.EntityFrameworkCore;
using RestoAPP.DAL.Configurations;
using RestoAPP.DAL.Entities;
using RestoAPP.DAL.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL
{
    public class RestoDbContext : DbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Repas> Repas { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<VBooking> VBooking { get; set; } //utilisation du VBooking

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=JEREMY-ALISON;initial catalog=Restaurant;integrated security=true");
            optionsBuilder.UseSqlServer("Data Source=JEREMY-ALISON;initial catalog=Restaurant;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.ApplyConfiguration(new CategoryConfig());
            mb.ApplyConfiguration(new ClientConfig());
            mb.ApplyConfiguration(new ReservationConfig());
            mb.ApplyConfiguration(new RepasConfig());
            mb.Entity<VBooking>().ToView("VBooking").HasNoKey(); //creation du model de cette vue
        }
    }
}
