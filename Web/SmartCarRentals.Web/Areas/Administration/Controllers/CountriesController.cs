namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
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

        public CountriesController(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
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

        public async Task<IActionResult> All()
        {
            var countries = await this.countriesService.GetAllAsync();
            var viewModel = new CountriesAllViewModelCollection();

            foreach (var country in countries)
            {
                viewModel.Countries.Add(country.To<CountriesAllViewModel>());
            }

            return this.View(viewModel);
        }
    }
}
