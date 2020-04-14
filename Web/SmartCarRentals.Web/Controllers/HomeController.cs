namespace SmartCarRentals.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.TransfersTypes;
    using SmartCarRentals.Web.ViewModels;
    using SmartCarRentals.Web.ViewModels.Main.Cars;
    using SmartCarRentals.Web.ViewModels.Main.Home;
    using SmartCarRentals.Web.ViewModels.Main.TransfersTypes;

    public class HomeController : BaseController
    {
        private readonly IHomeService homeService;
        private readonly ITransfersTypesService transfersTypesService;

        public HomeController(
                              IHomeService homeService,
                              ITransfersTypesService transfersTypesService)
        {
            this.homeService = homeService;
            this.transfersTypesService = transfersTypesService;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await this.homeService.GetHotOffersCarsAsync();
            var viewModel = new CarsHotOffersViewModelCollection()
            {
                Cars = cars.Select(c => c.To<CarsHotOffersViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            var viewModel = new ContactViewModel();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel contactViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(contactViewModel);
            }

            return this.Redirect("/");
        }

        public async Task<IActionResult> Services()
        {
            var transfersTypes = await this.transfersTypesService.GetAllAsync<TransfersTypesServiceDropDownModel>();
            var viewModel = new TransfersTypesCollection()
            {
                TransfersTypes = transfersTypes.Select(t => t.To<TransfersTypesDropDownViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
