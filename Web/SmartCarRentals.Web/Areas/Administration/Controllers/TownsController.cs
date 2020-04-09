namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Countries;
    using SmartCarRentals.Services.Models.Administration.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Countries;
    using SmartCarRentals.Web.ViewModels.Administration.Towns;

    public class TownsController : AdministrationController
    {
        private const string DeleteErrorMessage = "Failed to delete the town.";

        private readonly ITownsService townsService;
        private readonly ICountriesService countriesService;
        private readonly IParkingsService parkingsService;
        private readonly IParkingSlotsService parkingSlotsService;

        public TownsController(
                               ITownsService townsService,
                               ICountriesService countriesService,
                               IParkingsService parkingsService,
                               IParkingSlotsService parkingSlotsService)
        {
            this.townsService = townsService;
            this.countriesService = countriesService;
            this.parkingsService = parkingsService;
            this.parkingSlotsService = parkingSlotsService;
        }

        public async Task<IActionResult> Create()
        {
            var countries = await this.countriesService.GetAllAsync<CountriesServiceDropDownModel>();
            var viewModel = new TownInputModel();
            viewModel.Countries = countries.Select(c => c.To<CountriesDropDownViewModel>()).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TownInputModel townInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(townInputModel);
            }

            var townServiceModel = townInputModel.To<TownServiceInputModel>();
            await this.townsService.CreateAsync(townServiceModel);

            return this.Redirect("/Administration/Towns/All");
        }

        public async Task<IActionResult> Details(int id)
        {
            var town = await this.townsService.GetByIdAsync<TownServiceDetailsModel>(id);
            var viewModel = town.To<TownDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var town = await this.townsService.GetByIdAsync<TownServiceDetailsModel>(id);
            var viewModel = town.To<TownDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TownDetailsViewModel driverDetailsViewModel)
        {
            var serviceDriver = driverDetailsViewModel.To<TownServiceDetailsModel>();

            await this.townsService.EditAsync(serviceDriver);

            return this.Redirect($"/Administration/Towns/Details/{serviceDriver.Id}");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var town = await this.townsService.GetByIdAsync<TownServiceDetailsModel>(id);
            var viewModel = town.To<TownDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/Towns/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var townParkings = await this.parkingsService.GetAllByTownIdAsync(id);
            var parkingsIds = townParkings.Select(p => p.Id).ToList();

            var parkingSlots = await this.parkingSlotsService.GetAllByTownIdAsync(id);
            var parkingSlotsIds = parkingSlots.Select(ps => ps.Id).ToList();

            await this.parkingSlotsService.DeleteAllByIdAsync(parkingSlotsIds);
            await this.parkingsService.DeleteAllByIdAsync(parkingsIds);

            if (!(await this.townsService.DeleteByIdAsync(id) > 0))
            {
                this.TempData["Error"] = DeleteErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            return this.Redirect("/Administration/Towns/All");
        }

        public async Task<IActionResult> ByCountry(int id)
        {
            var towns = await this.townsService.GetAllByCountryIdAsync(id);
            var viewModel = new TownsAllViewModelCollection()
            {
                Towns = towns.Select(t => t.To<TownsAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var towns = await this.townsService.GetAllAsync<TownsServiceAllModel>();
            var viewModel = new TownsAllViewModelCollection()
            {
                Towns = towns.Select(t => t.To<TownsAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }
    }
}
