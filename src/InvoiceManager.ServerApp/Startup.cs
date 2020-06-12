using AutoMapper;
using InvoiceManager.Core.Repositories;
using InvoiceManager.Core.Services;
using InvoiceManager.ServerApp.Mapper;
using InvoiceManager.ServerApp.Models;
using InvoiceManager.ServerApp.Services;
using InvoiceManager.SqlServer;
using InvoiceManager.SqlServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvoiceManager.ServerApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("db"));
            });

            services.AddAutoMapper(typeof(ViewMappingProfile));

            services.AddScoped<IInvoiceRepository, InvoiceRepository>( );
            services.AddScoped<IInvoiceItemRepository, InvoiceItemRepository>( );
            services.AddScoped<IInvoiceService<InvoiceViewModel>, InvoiceService>( );
            services.AddScoped<IInvoiceItemService<InvoiceItemViewModel>, InvoiceItemService>( );
            services.AddControllersWithViews( );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment( ))
            {
                app.UseDeveloperExceptionPage( );
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                app.UseHsts( );
            }
            app.UseHttpsRedirection( );
            app.UseStaticFiles( );

            app.UseRouting( );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers( );
            });
        }
    }
}
