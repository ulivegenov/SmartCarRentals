namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Web.ViewModels.Administration.Dashboard;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class DashboardController : AdministrationController
    {
        private readonly IParkingsService parkingsService;
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;

        public DashboardController(
                                   IParkingsService parkingsService,
                                   ICountriesService countriesService,
                                   ITownsService townsService)
        {
            this.parkingsService = parkingsService;
            this.countriesService = countriesService;
            this.townsService = townsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                ParkingsCount = await this.parkingsService.GetCountAsync(),
                CountriesCount = await this.countriesService.GetCountAsync(),
                TownsCount = await this.townsService.GetCountAsync(),
            };

            return this.View(viewModel);
        }
    }
}
