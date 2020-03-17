namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;
    using SmartCarRentals.Services.Models.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class ParkingsController : AdministrationController
    {
        private readonly IParkingsService parkingsService;
        private readonly ITownsService townsService;

        public ParkingsController(
                                  IParkingsService parkingsService,
                                  ITownsService townsService)
        {
            this.parkingsService = parkingsService;
            this.townsService = townsService;
        }

        public async Task<IActionResult> Create()
        {
            var towns = await this.townsService.GetAllAsync();
            this.AddListItemsToViewBag(towns);

            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingInputModel parkingInputModel)
        {
            var towns = await this.townsService.GetAllAsync();
            this.AddListItemsToViewBag(towns);

            if (!this.ModelState.IsValid)
            {
                return this.View(parkingInputModel);
            }

            var parkingServiceModel = parkingInputModel.To<ParkingServiceInputModel>();

            parkingServiceModel.Town = this.townsService.GetByName(parkingInputModel.Town);
            await this.parkingsService.CreateAsync(parkingServiceModel);

            return this.Redirect("/Administration/Parkings/All");
        }

        public async Task<IActionResult> All()
        {
            var parkings = await this.parkingsService.GetAllAsync();
            var viewModel = new ParkingsAllViewModelCollection();

            foreach (var parking in parkings)
            {
                viewModel.Parkings.Add(parking.To<ParkingsAllViewModel>());
            }

            return this.View(viewModel);
        }

        private void AddListItemsToViewBag(IEnumerable<TownsServiceAllModel> towns)
        {
            this.ViewBag.Towns = new List<SelectListItem>();

            foreach (var town in towns)
            {
                var listitem = new SelectListItem
                {
                    Text = town.Name,
                    Value = town.Name,
                };

                this.ViewBag.Towns.Add(listitem);
            }
        }
    }
}
