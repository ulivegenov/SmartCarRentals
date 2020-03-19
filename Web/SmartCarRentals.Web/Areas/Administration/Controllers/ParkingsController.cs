namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;
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
            var towns = await this.townsService.GetAllAsync<TownsDropDownViewModel>();
            var viewModel = new ParkingInputModel()
            {
                Towns = towns,
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

        public async Task<IActionResult> Details(int id)
        {
            var parking = await this.parkingsService.GetByIdAsync(id);
            var viewModel = parking.To<ParkingDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var parking = await this.parkingsService.GetByIdAsync(id);
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

        public async Task<IActionResult> All()
        {
            var parkings = await this.parkingsService.GetAllAsync();
            var viewModel = new ParkingsAllViewModelCollection();

            foreach (var parking in parkings)
            {
                viewModel.Parkings.Add(parking.To<ParkingsAllViewModel>());
            }

            return this.View(viewModel);
        }
    }
}
