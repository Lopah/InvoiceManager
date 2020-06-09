using InvoiceManager.SqlServer.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer
{
    public static class AppDbContextSeed
    {
        public static async Task SeedSampleData(AppDbContext context)
        {
            if (context.Invoices.Any())
            {
                return;
            }
            await context.Invoices.AddRangeAsync(
                new InvoiceDto
                {
                    Id = 0,
                    Paid = false,
                    InvoiceItems = new List<ItemDto>
                    {
                        new ItemDto
                        {
                            Name = "Item1",
                            Value = 200
                        },
                        new ItemDto
                        {
                            Name = "Item2",
                            Value = 300
                        },
                        new ItemDto
                        {
                            Name = "Item3",
                            Value = 400
                        },
                        new ItemDto
                        {
                            Name = "Item4",
                            Value = 500
                        },
                        new ItemDto
                        {
                            Name = "Item5",
                            Value = 600
                        }
                    }
                },
                new InvoiceDto
                {
                    Id = 1,
                    Paid = true,
                    InvoiceItems = new List<ItemDto>
                    {
                        new ItemDto
                        {
                            Name = "PaidItem1",
                            Value = 500
                        }
                    }
                }
                );

            await context.SaveChangesAsync( );
        }
    }
}
