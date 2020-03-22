namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Services.Models.Parkings;
    using SmartCarRentals.Web.ViewModels.Administration.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarsController : AdministrationController
    {
        private readonly ICarsService carsService;
        private readonly IParkingsService parkingsService;
        private readonly IParkingSlotsService parkingSlotsService;

        public CarsController(
                              ICarsService carsService,
                              IParkingsService parkingsService,
                              IParkingSlotsService parkingSlotsService)
        {
            this.carsService = carsService;
            this.parkingsService = parkingsService;
            this.parkingSlotsService = parkingSlotsService;
        }

        public async Task<IActionResult> Create()
        {
            var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDawnModel>();
            var viewModel = new CarInputModel();
            viewModel.Parkings = parkings.Select(p => p.To<ParkingsDropDownViewModel>()).ToList();

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var cars = await this.carsService.GetAllAsync<CarsServiceAllModel>();

            var viewModel = new CarsAllViewModelCollection();

            foreach (var car in cars)
            {
                viewModel.Cars.Add(car.To<CarsAllViewModel>());
            }

            return this.View(viewModel);
        }
    }
}
