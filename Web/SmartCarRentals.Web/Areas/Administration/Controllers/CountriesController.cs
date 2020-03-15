namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data;
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

            var countryServiceModel = countryInputModel.To<CountryServicesInputModel>();

            await this.countriesService.CreateAsync(countryServiceModel);

            return this.Redirect("/Administration/Countries/All");
        }

        public IActionResult All()
        {
            return this.View();
        }
    }
}
