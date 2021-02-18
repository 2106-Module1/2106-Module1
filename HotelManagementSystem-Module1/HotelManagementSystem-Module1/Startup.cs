using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem_Module1.Domain.Models;
using HotelManagementSystem_Module1.DataSource;
using HotelManagementSystem_Module1.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelManagementSystem_Module1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            /* Add all services and dependency injections below */

            //Team 4 services


            //Team 6 services


            //Team 9 services
            //Use local MSSQL database
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;AttachDbFileName=C:\\Users\\WenLong\\Documents\\ICT2106.mdf"));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IGuestRepository, GuestRepository>(); 
            services.AddScoped<IFacilityReservationRepository, FacilityReservationRepository>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IFacilityReservationService, FacilityReservationService>();
            services.AddScoped<IRoom, RoomManagement>();
            services.AddScoped<IRoomGateway, RoomGateway>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
