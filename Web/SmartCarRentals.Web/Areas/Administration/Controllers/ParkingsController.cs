﻿namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;
    using SmartCarRentals.Services.Models.Administration.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;
    using SmartCarRentals.Web.ViewModels.Administration.Towns;

    public class ParkingsController : AdministrationController
    {
        private const string DeleteErrorMessage = "Failed to delete the parking.";

        private readonly ITownsService townsService;
        private readonly IParkingsService parkingsService;
        private readonly IParkingSlotsService parkingSlotsService;

        public ParkingsController(
                                  ITownsService townsService,
                                  IParkingsService parkingsService,
                                  IParkingSlotsService parkingSlotsService)
        {
            this.parkingSlotsService = parkingSlotsService;
            this.parkingsService = parkingsService;
            this.townsService = townsService;
        }

        public async Task<IActionResult> Create()
        {
            var towns = await this.townsService.GetAllAsync<TownsServiceDropDownModel>();
            var viewModel = new ParkingInputModel()
            {
                Towns = towns.Select(t => t.To<TownsDropDownViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParkingInputModel parkingInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(parkingInputModel);
            }

            var parkingServiceModel = parkingInputModel.To<ParkingServiceInputModel>();
            await this.parkingsService.CreateAsync(parkingServiceModel);

            return this.Redirect("/Administration/Parkings/All");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var parking = await this.parkingsService.GetByIdAsync<ParkingServiceDetailsModel>(id);
            var viewModel = parking.To<ParkingDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ParkingDetailsViewModel parkingDetailsViewModel)
        {
            var serviceDriver = parkingDetailsViewModel.To<ParkingServiceDetailsModel>();

            await this.parkingsService.EditAsync(serviceDriver);

            return this.Redirect($"/Administration/Parkings/Details/{serviceDriver.Id}");
        }

        public async Task<IActionResult> Details(int id)
        {
            var parking = await this.parkingsService.GetByIdAsync<ParkingServiceDetailsModel>(id);
            var viewModel = parking.To<ParkingDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var parking = await this.parkingsService.GetByIdAsync<ParkingServiceDetailsModel>(id);
            var viewModel = parking.To<ParkingDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/Parkings/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var parkingSlots = await this.parkingSlotsService.GetAllByParkingIdAsync(id);
            var parkingSlotsIds = parkingSlots.Select(ps => ps.Id).ToList();

            await this.parkingSlotsService.DeleteAllByIdAsync(parkingSlotsIds);

            if (!(await this.parkingsService.DeleteByIdAsync(id) > 0))
            {
                this.TempData["Error"] = DeleteErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            return this.Redirect("/Administration/Parkings/All");
        }

        public async Task<IActionResult> ByTown(int id)
        {
            var parkings = await this.parkingsService.GetAllByTownIdAsync(id);
            var viewModel = new ParkingsAllViewModelCollection()
            {
                Parkings = parkings.Select(p => p.To<ParkingsAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> All(int id = 1)
        {
            var page = id;
            var parkings = await this.parkingsService.GetAllWithPagingAsync<ParkingsServiceAllModel>(GlobalConstants.ItemsPerPageAdmin, (page - 1) * GlobalConstants.ItemsPerPageAdmin);
            var viewModel = new ParkingsAllViewModelCollection()
            {
                Parkings = parkings.Select(p => p.To<ParkingsAllViewModel>()).ToList(),
            };

            var count = await this.parkingsService.GetCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPageAdmin);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
