using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TruckRegistration.Domain;
using TruckRegistration.Exceptions;
using TruckRegistration.Infrastructure;

namespace TruckRegistration
{
    public class Startup
    {
        /// <summary>
        /// This method gets called by the runtime. It is used to add services to the container.
        /// The services added are:
        ///   - DataBaseContext, poitint to the DefaultConnection value in the appsettings.json
        ///   - Controllers with a very simple custom exception filter for all the requests
        ///   - Two main classes registered for dependency injection, scoped at the request lifetime.
        ///   - React static files, to be served from the ClientApp/build directory
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection")
            );

            services.AddControllers(options => options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<TruckService>();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        /// <summary>
        /// This method gets called by the runtime. It is used to configure the HTTP request pipeline.
        /// The middleware services added to the pipeline are:
        ///  - A service scope to ensure the database gets created at run time and migrated to the latest version
        ///  - The exception handler path
        ///  - HTTPs redirection
        ///  - React static files
        ///  - Default Routing
        ///  - Endpoints to the controllers
        ///  - React source path and npmScript
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
