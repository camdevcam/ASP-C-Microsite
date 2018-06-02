using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DevExpress.Xpo.Demo.Core;
using DevExpress.Xpo.Demo.Entities;

namespace DevExpress.Xpo.Demo
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
            services.AddXpoDefaultUnitOfWork(true, options =>
                options.UseConnectionString(Configuration.GetConnectionString("SQLite"))
                    .UseConnectionPool(false)
                    .UseThreadSafeDataLayerSchemaInitialization(true)
                    .UseAutoCreationOption(DB.AutoCreateOption.DatabaseAndSchema)
                    .UseEntityTypes(new Type[] { typeof(User) })
            );

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseXpoDemoData();

            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.Use(async (context, next) => {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/404";
                    await next();
                }
            });

            app.UseMvcWithDefaultRoute();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
