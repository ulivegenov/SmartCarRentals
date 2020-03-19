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
