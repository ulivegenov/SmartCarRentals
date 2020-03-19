namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;

    public class DriversController : AdministrationController
    {
        private readonly IDriversService driversService;

        public DriversController(IDriversService driversService)
        {
            this.driversService = driversService;
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
