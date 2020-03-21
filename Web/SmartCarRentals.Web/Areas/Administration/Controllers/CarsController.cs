namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Cars;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarsController : AdministrationController
    {
        private readonly ICarsService carsService;

        public CarsController(ICarsService carsService)
        {
            this.carsService = carsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        //public async Task<IActionResult> All()
        //{
        //    var cars = await this.carsService.GetAllAsync<CarsServiceAllModel>();

        //    var viewModel = new CarsAllViewModelCollection();

        //    return this.View(viewModel);
        //}
    }
}
