using InvoiceManager.SqlServer.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManager.SqlServer
{
    public static class AppDbSeed
    {
        public static async Task SeedSampleDataAsync(AppDbContext context)
        {
            if (context.Invoices.Any())
            {
                return;
            }

            await context.Invoices.AddRangeAsync(
                new List<InvoiceDto>
                {
                    new InvoiceDto
                    {
                        Id = 0,
                        Items = new List<ItemDto>
                        {
                            new ItemDto
                            {
                                Name = "Name1",
                                Price = 100,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Price = 150,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Price = 200,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Price = 300,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Price = 400,
                            },
                        },
                    },
                    new InvoiceDto
                        {
                            Id = 1,
                            Paid = true,
                            Items = new List<ItemDto>
                            {
                                new ItemDto
                                {
                                    Name = "SomeItem",
                                    Price = 1000
                                }
                            }
                        }
                });
        }
    }
}
