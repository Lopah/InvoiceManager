using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceDto>
    {
        public void Configure(EntityTypeBuilder<InvoiceDto> builder)
        {
            builder.Property(e => e.Paid)
                .IsRequired( );

            builder.HasMany(e => e.InvoiceItems)
                .WithOne(e => e.Invoice)
                .HasForeignKey(e => e.InvoiceId);
        }
    }
}
