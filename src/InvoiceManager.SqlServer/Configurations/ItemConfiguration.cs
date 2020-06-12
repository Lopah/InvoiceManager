using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<ItemDto>
    {
        public void Configure(EntityTypeBuilder<ItemDto> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired( )
                .HasMaxLength(200);

        }
    }
}
