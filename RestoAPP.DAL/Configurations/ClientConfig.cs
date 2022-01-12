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
    class ClientConfig : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
            builder.Property(c => c.UserLevel).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Tel).HasMaxLength(50);
        }

    }
}
