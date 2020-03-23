namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Services.Models.Parkings;
    using SmartCarRentals.Web.ViewModels.Administration.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class CarsController : AdministrationController
    {
        private const string DeleteErrorMessage = "Failed to delete the parking.";

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
            var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDownModel>();
            var viewModel = new CarInputModel();
            viewModel.Parkings = parkings.Select(p => p.To<ParkingsDropDownViewModel>()).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarInputModel carInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(carInputModel);
            }

            var carServiceModel = carInputModel.To<CarServiceInputModel>();
            await this.carsService.CreateAsync(carServiceModel);

            return this.Redirect("/Administration/Cars/All");
        }

        public async Task<IActionResult> Edit(string id)
        {
            var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDownModel>();
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var viewModel = car.To<CarDetailsViewModel>();
            viewModel.Parkings = parkings.Select(p => p.To<ParkingsDropDownViewModel>()).ToList();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarDetailsViewModel carDetailsViewModel)
        {
            var serviceCar = carDetailsViewModel.To<CarServiceDetailsModel>();

            if (!this.ModelState.IsValid)
            {
                var parkings = await this.parkingsService.GetAllAsync<ParkingsServiceDropDownModel>();
                carDetailsViewModel.Parkings = parkings.Select(p => p.To<ParkingsDropDownViewModel>()).ToList();

                return this.View(carDetailsViewModel);
            }

            await this.carsService.EditAsync(serviceCar);

            return this.Redirect($"/Administration/Cars/Details/{serviceCar.Id}");
        }

        public async Task<IActionResult> Details(string id)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var carParking = await this.parkingsService.GetByCarIdAsync(id);
            car.Parking = carParking;
            var viewModel = car.To<CarDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var viewModel = car.To<CarDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/Cars/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            if (!(await this.carsService.DeleteByIdAsync(id) > 0))
            {
                this.TempData["Error"] = DeleteErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            return this.Redirect("/Administration/Cars/All");
        }

        public async Task<IActionResult> All()
        {
            var cars = await this.carsService.GetAllAsync<CarsServiceAllModel>();

            var viewModel = new CarsAllViewModelCollection();

            viewModel.Cars = cars.Select(c => c.To<CarsAllViewModel>()).ToList();

            return this.View(viewModel);
        }
    }
}
