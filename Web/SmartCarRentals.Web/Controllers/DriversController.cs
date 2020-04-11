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
            var drivers = this.driversService.GetAllWithPaging<DriversServiceAllModel>(GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);
            var driversWithRatings = await this.driversService.GetAllAsync<DriversServiceAllModel>();

            var viewModel = new DriversAllViewModelCollection();

            foreach (var driver in drivers)
            {
                driver.Rating = driversWithRatings.Where(d => d.Id == driver.Id)
                                                  .Select(d => d.Rating)
                                                  .FirstOrDefault();

                viewModel.Drivers.Add(driver.To<DriversAllViewModel>());
            }

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
