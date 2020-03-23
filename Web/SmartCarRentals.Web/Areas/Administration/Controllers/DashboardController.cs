namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Web.ViewModels.Administration.Dashboard;

    public class DashboardController : AdministrationController
    {
        private readonly IParkingsService parkingsService;
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;
        private readonly IDriversService driversService;
        private readonly ICarsService carsService;
        private readonly IUsersService usersService;

        public DashboardController(
                                   IParkingsService parkingsService,
                                   ICountriesService countriesService,
                                   ITownsService townsService,
                                   IDriversService driversService,
                                   ICarsService carsService,
                                   IUsersService usersService)
        {
            this.parkingsService = parkingsService;
            this.countriesService = countriesService;
            this.townsService = townsService;
            this.driversService = driversService;
            this.carsService = carsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                ParkingsCount = await this.parkingsService.GetCountAsync(),
                CountriesCount = await this.countriesService.GetCountAsync(),
                TownsCount = await this.townsService.GetCountAsync(),
                DriversCount = await this.driversService.GetCountAsync(),
                CarsCount = await this.carsService.GetCountAsync(),
                UsersCount = await this.usersService.GetCountAsync(),
            };

            return this.View(viewModel);
        }
    }
}
