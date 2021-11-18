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
    class RepasConfig : IEntityTypeConfiguration<Repas>
    {
        public void Configure(EntityTypeBuilder<Repas> builder)
        {
            builder.Property(r => r.Plat).IsRequired().HasMaxLength(255);
            builder.Property(r => r.Description).HasMaxLength(255);
            builder.Property(r => r.Prix).IsRequired();
            builder.Property(r => r.CategoryId).IsRequired();

            builder.HasOne(m => m.Category)
            .WithMany(c => c.Repas)
            .OnDelete(DeleteBehavior.SetNull)
            .HasForeignKey(m => m.CategoryId);
        }
    }
}
