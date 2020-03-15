namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data;
    using SmartCarRentals.Web.ViewModels.Administration.Dashboard;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class DashboardController : AdministrationController
    {
        private readonly IParkingsService parkingsService;

        public DashboardController(IParkingsService parkingsService)
        {
            this.parkingsService = parkingsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel
            {
                ParkingsCount = await this.parkingsService.GetCountAsync(),
            };

            return this.View(viewModel);
        }
    }
}
