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
    using SmartCarRentals.Services.Models.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Towns;

    public class TownsController : AdministrationController
    {
        private readonly ITownsService townsService;
        private readonly ICountriesService countriesService;

        public TownsController(ITownsService townsService, ICountriesService countriesService)
        {
            this.townsService = townsService;
            this.countriesService = countriesService;
        }

        public async Task<IActionResult> Create()
        {
            var countries = await this.countriesService.GetAllAsync();
            this.ViewBag.Countries = new List<SelectListItem>();

            foreach (var country in countries)
            {
                var listitem = new SelectListItem
                {
                    Text = country.Name,
                    Value = country.Name,
                };

                this.ViewBag.Countries.Add(listitem);
            }

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TownInputModel townInputModel)
        {
            var countries = await this.countriesService.GetAllAsync();
            this.ViewBag.Countries = new List<SelectListItem>();

            foreach (var country in countries)
            {
                var listitem = new SelectListItem
                {
                    Text = country.Name,
                    Value = country.Name,
                };

                this.ViewBag.Countries.Add(listitem);
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(townInputModel);
            }

            var townServiceModel = townInputModel.To<TownServiceInputModel>();

            townServiceModel.Country = this.countriesService.GetByName(townInputModel.Country);
            await this.townsService.CreateAsync(townServiceModel);

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
