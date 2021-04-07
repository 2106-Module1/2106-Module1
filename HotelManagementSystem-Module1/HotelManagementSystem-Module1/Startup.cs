using HotelManagementSystem.Data.ConControls;
using HotelManagementSystem.Data.ConInterfaces;
using HotelManagementSystem.Data.Mod2Repository;
using HotelManagementSystem.Data.PaymentGateways;
using HotelManagementSystem.Data.PaymentInterfaces;
using HotelManagementSystem.DataSource;
using HotelManagementSystem.Domain;
using HotelManagementSystem.Domain.Models;
using HotelManagementSystem.Models.ConControls;
using HotelManagementSystem.Models.ConInterfaces;
using HotelManagementSystem.Models.PaymentControls;
using HotelManagementSystem.Models.PaymentInterfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            /* Add all services and dependency injections below */

            //Team 4 services
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();
            services.AddScoped<IPromoCodeService, PromoCodeService>();
            services.AddScoped<IReservationValidator, ReservationValidator>();
            services.AddScoped<IReservationDirector, ReservationDirector>();

            //Team 6 services
            services.AddScoped<IPinRepository, PinRepository>();
            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IRoom, RoomManagement>();
            services.AddScoped<IStaffGateway, StaffGateway>();
            services.AddScoped<IRoomGateway, RoomGateway>();
            services.AddSingleton<IHostedService, TimerEventService>();
            services.AddScoped<IRoomFacade, RoomFacade>();
            services.AddScoped<IAuthenticate, Authenticate>();
            services.AddScoped<IStaff, Staff>();


            //Team 9 services
            //Use local MSSQL database
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ICT2106Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=True"));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IGuestRepository, GuestRepository>();
            services.AddScoped<IFacilityReservationRepository, FacilityReservationRepository>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<IFacilityReservationService, FacilityReservationService>();

            //External teams
            services.AddScoped<IPublicArea, PublicArea>();

            // Team 2 services
            services.AddScoped<IShuttleScheduleDAO, ShuttleScheduleGateway>();
            services.AddScoped<IShuttleBusDAO, ShuttleBusGateway>();
            services.AddScoped<IShuttlePassengerDAO, ShuttlePassengerGateway>();
            services.AddScoped<IRestReservationDAO, RestReservationGateway>();
            services.AddScoped<ITourReservationDAO, TourReservationGateway>();
            services.AddScoped<ITaxiReservationDAO, TaxiReservationGateway>();

            services.AddScoped<IShuttleServices, ShuttleService>();
            services.AddScoped<IShuttleBusServices, ShuttleBusService>();
            services.AddScoped<IShuttleBusPassengerServices, ShuttleBusPassengerService>();
            services.AddScoped<IRestServices, RestBookingService>();
            services.AddScoped<ITaxiServices, TaxiBookingService>();
            services.AddScoped<ITourServices, TourBookingService>();

            // Team 7 services
            services.AddScoped<iReservationInvoiceGateway, ReservationInvoiceGateway>();
            services.AddScoped<iReservationInvoice, ReservationInvoiceControl>();
            services.AddScoped<iPostChargeGateway, PostChargeGateway>();
            services.AddScoped<iPostCharge, PostChargeControl>();
            /*services.AddTransient<iCheckout, CheckoutAdapter>();*/

            // Mod 2 local MSSQL database and Services
            services.AddDbContext<Mod2Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Mod2Context")));
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

            app.UseSession();

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
                endpoints.MapControllerRoute(
                    name: "Transport Reservation",
                    pattern: "{controller=TransportReservation}/{action=TransportReservation}/{id?}");
            });
        }
    }
}
