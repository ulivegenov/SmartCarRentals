namespace SmartCarRentals.Services.Data.Tests.Common.Seeders
{
    using System;
    using System.Reflection;

    using CloudinaryDotNet;

    using Hangfire;
    using Hangfire.MemoryStorage;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using SmartCarRentals.Data;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.AppServices;
    using SmartCarRentals.Services.Data.AppServices.Contracts;
    using SmartCarRentals.Services.Data.Main;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Messaging;
    using SmartCarRentals.Services.Models.Administration.Countries;
    using SmartCarRentals.Web.ViewModels.Administration.Countries;

    public abstract class BaseServiceTestsOptions : IDisposable
    {
        protected BaseServiceTestsOptions()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMemoryStorage());

            // Cloudinary Api set
            Account cloudinaryCredentials = new Account(
                                                        "Cloudinary:CloudName",
                                                        "Cloudinary:ApiKey",
                                                        "Cloudinary:ApiSecret");

            Cloudinary cloudinary = new Cloudinary(cloudinaryCredentials);

            services
                 .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                 {
                     options.Password.RequireDigit = false;
                     options.Password.RequireLowercase = false;
                     options.Password.RequireUppercase = false;
                     options.Password.RequireNonAlphanumeric = false;
                     options.Password.RequiredLength = 6;
                 })
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient(typeof(ILoggerFactory), typeof(LoggerFactory));
            services.AddTransient<IEmailSender>(x => new SendGridEmailSender("SendGridKey"));
            services.AddTransient<ISettingsService, SettingsService>();
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

            // AutoMapper
            AutoMapperConfig.RegisterMappings(typeof(CountriesServiceAllModel).GetTypeInfo().Assembly);

            // SignalR Setup
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }
    }
}
