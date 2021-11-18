using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestoAPP.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoAPP.DAL.Configurations
{
    class ReservationConfig : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.Property(res => res.NbPers).IsRequired();
            builder.Property(res => res.ServiceReservation).IsRequired().HasMaxLength(50);
            builder.Property(res => res.DateDeRes).HasColumnType("Date").IsRequired();
            builder.Property(res => res.IdClient).IsRequired();
            builder.Property(res => res.ValidationStatuts).IsRequired();


            //builder.HasOne(res => res.ValidationStatuts).WithMany(res => res.Reservations).OnDelete(DeleteBehavior.SetNull).HasForeignKey(p => p.IdValidationStatus);

            builder.HasOne(res => res.Client).WithMany(res => res.Reservation).OnDelete(DeleteBehavior.Cascade).HasForeignKey(p => p.IdClient);
        }
    }
}
