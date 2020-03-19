namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;
    using SmartCarRentals.Services.Models.Towns;
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
            var countries = await this.countriesService.GetAllAsync<CountriesDropDownViewModel>();
            var viewModel = new TownInputModel()
            {
                Countries = countries,
            };

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
            var town = await this.townsService.GetByIdAsync(id);
            var viewModel = town.To<TownDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var town = await this.townsService.GetByIdAsync(id);
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

        public async Task<IActionResult> All()
        {
            var towns = await this.townsService.GetAllAsync();
            var viewModel = new TownsAllViewModelCollection();

            foreach (var town in towns)
            {
                viewModel.Towns.Add(town.To<TownsAllViewModel>());
            }

            return this.View(viewModel);
        }
    }
}
