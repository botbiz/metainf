using metainf.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace metainf
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
            services.AddMvc();

            switch (Configuration.GetValue<string>("MetaDB"))
            {
                case "SqlServer":
                    services.AddDbContext<MainContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase")));
                    break;
                case "Sqlite":
                    services.AddDbContext<MainContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultDatabase")));
                    break;
                case "MySql":
                    //services.AddDbContext<MainContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultDatabase")));
                    break;
                case "Postgres":
                    //services.AddDbContext<MainContext>(options => options.UsePostgres(Configuration.GetConnectionString("DefaultDatabase")));
                    break;
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
