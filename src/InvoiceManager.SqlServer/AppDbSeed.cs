using InvoiceManager.SqlServer.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InvoiceManager.SqlServer
{
    public static class AppDbSeed
    {
        public static void SeedSampleData(IServiceProvider provider)
        {
            using (var context = new AppDbContext(provider
                .GetRequiredService<DbContextOptions<AppDbContext>>( )))
            {
                if (context.Invoices.Any( ))
                {
                    return;
                }

                context.Invoices.AddRange(
                    new List<InvoiceDto>
                    {
                    new InvoiceDto
                    {
                        InvoiceItems = new List<ItemDto>
                        {
                            new ItemDto
                            {
                                Name = "Name1",
                                Value = 100,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Value = 150,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Value = 200,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Value = 300,
                            },

                            new ItemDto
                            {
                                Name = "Name1",
                                Value = 400,
                            },
                        },
                    },
                    new InvoiceDto
                        {
                            Paid = true,
                            InvoiceItems = new List<ItemDto>
                            {
                                new ItemDto
                                {
                                    Name = "SomeItem",
                                    Value = 1000
                                }
                            }
                        }
                    });

                context.SaveChanges( );
            }

        }
    }
}
