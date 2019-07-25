using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Storefront.ApiGateway.Authorization;
using Storefront.ApiGateway.Models.DataModel;

namespace Storefront.ApiGateway
{
    public class Startup
    {
        public readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddJwtAuthentication(_configuration.GetSection("Auth"));

            services.AddDefaultCorsPolicy();

            services.AddIdentity();

            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseNpgsql(_configuration["PostgreSQL:ConnectionString"], pgsql =>
                {
                    pgsql.MigrationsHistoryTable(tableName: "__migration_history", schema: ApiDbContext.Schema);
                });
            });

            services.AddOcelot(_configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseOcelot().Wait();
        }
    }
}
