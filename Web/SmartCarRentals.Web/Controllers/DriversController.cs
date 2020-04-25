namespace SmartCarRentals.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Drivers;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;

    public class DriversController : BaseController
    {
        private readonly IDriversService driversService;

        public DriversController(IDriversService driversService)
        {
            this.driversService = driversService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            var page = id;
            var drivers = await this.driversService.GetAllWithPagingAsync<DriversServiceAllModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var viewModel = new DriversAllViewModelCollection();
            viewModel.Drivers = drivers.Select(d => d.To<DriversAllViewModel>()).ToList();

            var count = await this.driversService.GetCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
