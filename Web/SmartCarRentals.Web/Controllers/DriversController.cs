namespace SmartCarRentals.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;

    public class DriversController : BaseController
    {
        private readonly IDriversService driversService;

        public DriversController(IDriversService driversService)
        {
            this.driversService = driversService;
        }

        public async Task<IActionResult> All()
        {
            var drivers = await this.driversService.GetAllAsync<DriversServiceAllModel>();

            var viewModel = new DriversAllViewModelCollection()
            {
                Drivers = drivers.Select(d => d.To<DriversAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }
    }
}
