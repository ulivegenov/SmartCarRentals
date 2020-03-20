namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;

    public class DriversController : AdministrationController
    {
        private const string DeleteErrorMessage = "Failed to delete the driver.";

        private readonly IDriversService driversService;
        private readonly IDriversRatingsService driversRatingsService;

        public DriversController(
                                 IDriversService driversService,
                                 IDriversRatingsService driversRatingsService)
        {
            this.driversService = driversService;
            this.driversRatingsService = driversRatingsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DriverInputModel driverInputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(driverInputModel);
            }

            var driverServiceModel = driverInputModel.To<DriverServiceInputModel>();

            await this.driversService.CreateAsync(driverServiceModel);

            return this.Redirect("/Administration/Drivers/All");
        }

        public async Task<IActionResult> Details(string id)
        {
            var driver = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(id);
            var viewModel = driver.To<DriverDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var driver = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(id);
            var viewModel = driver.To<DriverDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DriverDetailsViewModel driverDetailsViewModel)
        {
            var serviceDriver = driverDetailsViewModel.To<DriverServiceDetailsModel>();

            await this.driversService.EditAsync(serviceDriver);

            return this.Redirect($"/Administration/Drivers/Details/{serviceDriver.Id}");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var parking = await this.driversService.GetByIdAsync<DriverServiceDetailsModel>(id);
            var viewModel = parking.To<DriverDetailsViewModel>();

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("/Administration/Drivers/Delete/{id}")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            await this.driversRatingsService.DeleteAllByDriverIdAsync(id);

            if (!(await this.driversService.DeleteByIdAsync(id) > 0))
            {
                this.TempData["Error"] = DeleteErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            return this.Redirect("/Administration/Drivers/All");
        }

        public async Task<IActionResult> All()
        {
            var drivers = await this.driversService.GetAllAsync<DriversServiceAllModel>();
            var viewModel = new DriversAllViewModelCollection();

            foreach (var driver in drivers)
            {
                viewModel.Drivers.Add(driver.To<DriversAllViewModel>());
            }

            return this.View(viewModel);
        }
    }
}
