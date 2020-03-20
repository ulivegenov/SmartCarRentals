namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Web.ViewModels.Administration.Dashboard;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class DashboardController : AdministrationController
    {
        private readonly IParkingsService parkingsService;
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;
        private readonly IDriversService driversService;

        public DashboardController(
                                   IParkingsService parkingsService,
                                   ICountriesService countriesService,
                                   ITownsService townsService,
                                   IDriversService driversService)
        {
            this.parkingsService = parkingsService;
            this.countriesService = countriesService;
            this.townsService = townsService;
            this.driversService = driversService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                ParkingsCount = await this.parkingsService.GetCountAsync(),
                CountriesCount = await this.countriesService.GetCountAsync(),
                TownsCount = await this.townsService.GetCountAsync(),
                DriversCount = await this.driversService.GetCountAsync(),
            };

            return this.View(viewModel);
        }
    }
}
