using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InvoiceManager.SqlServer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }

        public DbSet<ItemDto> Items { get; set; }

        public DbSet<InvoiceDto> Invoices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly( ));
        }
    }
}
