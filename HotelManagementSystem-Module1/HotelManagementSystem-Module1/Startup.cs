using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelManagementSystem
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
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
            services.AddScoped<IPromoCodeService, PromoCodeService>();
            services.AddScoped<IReservationValidator, ReservationValidator>();

            //Team 6 services


            //Team 9 services
            //Use local MSSQL database
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ICT2106Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True"));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IGuestRepository, GuestRepository>(); 
            services.AddScoped<IFacilityReservationRepository, FacilityReservationRepository>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IFacilityReservationService, FacilityReservationService>();
            services.AddScoped<IRoom, RoomManagement>();
            services.AddScoped<IStaffGateway, StaffGateway>();
            services.AddScoped<IRoomGateway, RoomGateway>();
            services.AddSingleton<IHostedService, PinService>();

            //External teams
            services.AddScoped<IPublicArea, PublicArea>();
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
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Guest}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "View Reservation",
                    pattern: "{controller=Reservation}/{action=ReservationView}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Update Reservation",
                    pattern: "{controller=ReservationManagement}/{action=UpdateReservation}/{id?}");
                endpoints.MapControllerRoute(
                    name: "Create Reservation",
                    pattern: "{controller=ReservationCreation}/{action=CreateReservation}/{id?}");
                endpoints.MapControllerRoute(
                    name: "ReservationTrend",
                    pattern: "{controller=ReservationTrend}/{action=ReservationTrend}/{id?}");
                endpoints.MapControllerRoute(
                    name: "View Promo Code",
                    pattern: "{controller=PromoCode}/{action=PromoCodeView}/{id?}");
            });
        }
    }
}
