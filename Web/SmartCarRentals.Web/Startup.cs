namespace SmartCarRentals.Web
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;

    using Hangfire;
    using Hangfire.Common;
    using Hangfire.SqlServer;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using SendGrid;

    using SmartCarRentals.Data;
    using SmartCarRentals.Data.Common;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Data.Seeding;
    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.AppServices;
    using SmartCarRentals.Services.Data.AppServices.Contracts;
    using SmartCarRentals.Services.Data.Main;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Administration.Countries;
    using SmartCarRentals.Services.Models.Administration.Drivers;
    using SmartCarRentals.Services.Models.Administration.Parkings;
    using SmartCarRentals.Services.Models.Administration.Towns;
    using SmartCarRentals.Services.Models.Administration.Users;
    using SmartCarRentals.Web.Hubs;
    using SmartCarRentals.Web.Infrastructure;
    using SmartCarRentals.Web.ViewModels;
    using SmartCarRentals.Web.ViewModels.Main.Error;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Framework services
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();

            services.AddSession();

            services
                .AddMvc(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddRazorPagesOptions(options =>
                {
                    // options.AllowAreas = true;
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Register");
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(this.configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true,
            }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            // Cloudinary Api set
            Account cloudinaryCredentials = new Account(
                                        this.configuration["Cloudinary:CloudName"],
                                        this.configuration["Cloudinary:ApiKey"],
                                        this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinary);

            // SendGrid Api set
            SendGridClient sendGrid = new SendGridClient(this.configuration["Sendgrid:ApiKey"]);
            services.AddSingleton(sendGrid);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IParkingsService, ParkingsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<IParkingSlotsService, ParkingSlotsService>();
            services.AddTransient<IDriversService, DriversService>();
            services.AddTransient<IDriversRatingsService, DriversRatingsService>();
            services.AddTransient<ICarsService, CarsService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<ITransfersTypesService, TransfersTypesService>();
            services.AddTransient<ITransfersService, TransfersService>();
            services.AddTransient<IReservationsService, ReservationsService>();
            services.AddTransient<ITripsService, TripsService>();
            services.AddTransient<ICarsRatingsService, CarsRatingsService>();
            services.AddTransient<IMailService, SendGridService>();
            services.AddScoped<IHangfireService, HangfireService>();
            services.AddApplicationInsightsTelemetry();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IRecurringJobManager recurringJobs)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(CountryServiceInputModel).GetTypeInfo().Assembly,
                typeof(TownServiceInputModel).GetTypeInfo().Assembly,
                typeof(DriverServiceInputModel).GetTypeInfo().Assembly,
                typeof(ParkingServiceInputModel).GetTypeInfo().Assembly,
                typeof(CarServiceInputModel).GetTypeInfo().Assembly,
                typeof(UserServiceDetailsModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                dbContext.Database.Migrate();

                new ApplicationDbContextSeeder(this.configuration).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
            });

            // configure recurring jobs
            recurringJobs.AddOrUpdate("CancelExpiredReservations", Job.FromExpression<IHangfireService>(j => j.CancelExpiredReservationsAsync()), Cron.Daily());
            recurringJobs.AddOrUpdate("SettingUPTransfersStatusByDate", Job.FromExpression<IHangfireService>(j => j.SettingUpTransfersStatusByDateAsync()), Cron.Hourly());

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapHub<ChatHub>("/contact");
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default2", "{controller=Home}/{action=Index}/{id?}/{secondId?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
