namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration;

    public class ParkingsController : AdministrationController
    {
        private readonly IParkingsService parkingsService;

        public ParkingsController(IParkingsService parkingsService)
        {
            this.parkingsService = parkingsService;
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
