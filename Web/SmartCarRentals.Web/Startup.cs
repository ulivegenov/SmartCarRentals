namespace SmartCarRentals.Web
{
    using System.Reflection;

    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using SmartCarRentals.Data;
    using SmartCarRentals.Data.Common;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Data.Seeding;
    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Messaging;
    using SmartCarRentals.Services.Models.Countries;
    using SmartCarRentals.Services.Models.Drivers;
    using SmartCarRentals.Services.Models.Towns;
    using SmartCarRentals.Web.ViewModels;
    using SmartCarRentals.Web.ViewModels.Administration.Countries;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;
    using SmartCarRentals.Web.ViewModels.Administration.Towns;

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

            services.AddControllersWithViews();
            services.AddRazorPages();

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

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Cloudinary Api set
            Account cloudinaryCredentials = new Account(
                this.configuration["Cloudinary:CloudName"],
                this.configuration["Cloudinary:ApiKey"],
                this.configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);

            // Google Maps Api set
            Account googleMapsCredentials = new Account(this.configuration["GoogleMaps : ApiKey"]);

            services.AddSingleton(googleMapsCredentials);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IParkingsService, ParkingsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<IParkingSlotsService, ParkingSlotsService>();
            services.AddTransient<IDriversService, DriversService>();
            services.AddTransient<IDriversRatingsService, DriversRatingsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(ErrorViewModel).GetTypeInfo().Assembly,
                typeof(CountryInputModel).GetTypeInfo().Assembly,
                typeof(CountryServiceInputModel).GetTypeInfo().Assembly,
                typeof(TownInputModel).GetTypeInfo().Assembly,
                typeof(TownServiceInputModel).GetTypeInfo().Assembly,
                typeof(DriverInputModel).GetTypeInfo().Assembly,
                typeof(DriverServiceInputModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder(this.configuration).SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
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

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
