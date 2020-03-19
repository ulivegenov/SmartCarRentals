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
        private const string DeleteErrorMessage = "Failed to delete the country.";

        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;
        private readonly IParkingsService parkingsService;
        private readonly IParkingSlotsService parkingSlotsService;

        public CountriesController(
                                   ICountriesService countriesService,
                                   ITownsService townsService,
                                   IParkingsService parkingsService,
                                   IParkingSlotsService parkingSlotsService)
        {
            this.countriesService = countriesService;
            this.townsService = townsService;
            this.parkingsService = parkingsService;
            this.parkingSlotsService = parkingSlotsService;
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

        public async Task<IActionResult> Delete(int id)
        {
            var country = await this.countriesService.GetByIdAsync(id);
            var viewModel = country.To<CountryDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/Countries/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var countryTowns = await this.townsService.GetAllByCountryIdAsync(id);
            var townsIds = countryTowns.Select(c => c.Id).ToList();

            var countryParkings = await this.parkingsService.GetAllByCountryIdAsync(id);
            var parkingsIds = countryParkings.Select(p => p.Id).ToList();

            var parkingSlots = await this.parkingSlotsService.GetAllByCountryIdAsync(id);
            var parkingSlotsIds = parkingSlots.Select(ps => ps.Id).ToList();

            await this.parkingSlotsService.DeleteAllByIdAsync(parkingSlotsIds);
            await this.parkingsService.DeleteAllByIdAsync(parkingsIds);
            await this.townsService.DeleteAllByIdAsync(townsIds);

            if (!(await this.countriesService.DeleteByIdAsync(id) > 0))
            {
                this.TempData["Error"] = DeleteErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            return this.Redirect("/Administration/Countries/All");
        }

        public async Task<IActionResult> All()
        {
            var countries = await this.countriesService.GetAllAsync<CountriesServiceAllModel>();
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
