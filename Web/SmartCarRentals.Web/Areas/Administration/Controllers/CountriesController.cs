namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;
    using SmartCarRentals.Web.ViewModels.Administration.Countries;

    public class CountriesController : AdministrationController
    {
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;

        public CountriesController(
                                   ICountriesService countriesService,
                                   ITownsService townsService)
        {
            this.countriesService = countriesService;
            this.townsService = townsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CountryInputModel countryInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(countryInputModel);
            }

            var countryServiceModel = countryInputModel.To<CountryServiceInputModel>();

            await this.countriesService.CreateAsync(countryServiceModel);

            return this.Redirect("/Administration/Countries/All");
        }

        public async Task<IActionResult> Details(int id)
        {
            var country = await this.countriesService.GetByIdAsync(id);
            var viewModel = country.To<CountryDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var country = await this.countriesService.GetByIdAsync(id);
            var viewModel = country.To<CountryDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CountryDetailsViewModel countryDetailsViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(countryDetailsViewModel);
            }

            var countryServiceModel = countryDetailsViewModel.To<CountryServiceDetailsModel>();

            await this.countriesService.EditAsync(countryServiceModel);

            return this.Redirect($"/Administration/Countries/Details/{countryDetailsViewModel.Id}");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var country = await this.countriesService.GetByIdAsync(id);
            var viewModel = country.To<CountryDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/Categories/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            // TO DO LOGIC
            throw new NotImplementedException();
        }

            public async Task<IActionResult> All()
        {
            var countries = await this.countriesService.GetAllAsync();
            await this.townsService.GetAllAsync();

            var viewModel = new CountriesAllViewModelCollection();
            this.ViewBag.Parkings = new Dictionary<string, int>();

            foreach (var country in countries)
            {
                var parkingsCount = 0;

                foreach (var town in country.Towns)
                {
                    parkingsCount += town.Parkings.Count;
                }

                this.ViewBag.Parkings[country.Name] = parkingsCount;

                viewModel.Countries.Add(country.To<CountriesAllViewModel>());
            }

            return this.View(viewModel);
        }
    }
}
