using AutoMapper;
using InvoiceManager.API.Extensions;
using InvoiceManager.Core.Repositories;
using InvoiceManager.SqlServer;
using InvoiceManager.SqlServer.Mappings;
using InvoiceManager.SqlServer.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvoiceManager.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("db"));
            });

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IInvoiceRepository, InvoiceRepository>( );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment( ))
            {
                app.UseDeveloperExceptionPage( );
            }

            app.UseRouting( );

            //app.UseSecretKeyValidation( );

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers( );
            });
        }
    }
}
