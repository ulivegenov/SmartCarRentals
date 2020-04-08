namespace SmartCarRentals.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.Trips;
    using SmartCarRentals.Web.ViewModels.Main.Trips;

    public class TripsController : BaseController
    {
        private const string UnavailableCarErrorMessage = "Sorry! This Car is unavailable, at the moment.";

        private readonly ICarsService carsService;
        private readonly ITripsService tripsService;
        private readonly IParkingsService parkingsService;
        private readonly IUsersService usersService;
        private readonly UserManager<ApplicationUser> userManager;

        public TripsController(
                               ICarsService carsService,
                               ITripsService tripsService,
                               IParkingsService parkingsService,
                               IUsersService usersService,
                               UserManager<ApplicationUser> userManager)
        {
            this.carsService = carsService;
            this.tripsService = tripsService;
            this.parkingsService = parkingsService;
            this.usersService = usersService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create(string id)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);

            var viewModel = new TripCreateInputModel()
            {
                CarId = car.Id,
                Car = car.To<Car>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(TripCreateInputModel tripCreateInputModel, string id)
        {
            var carServiceModel = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var currentUser = await this.userManager.GetUserAsync(this.User);

            tripCreateInputModel.CarId = carServiceModel.Id;
            tripCreateInputModel.ClientId = currentUser.Id;

            if (carServiceModel.HireStatus == HireStatus.Unavailable)
            {
                this.TempData["Error"] = UnavailableCarErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            if (!this.ModelState.IsValid)
            {
                tripCreateInputModel.CarId = carServiceModel.Id;
                tripCreateInputModel.ClientId = currentUser.Id;

                return this.View(tripCreateInputModel);
            }

            var tripServiceCreateModel = tripCreateInputModel.To<TripServiceInputModel>();
            await this.tripsService.CreateAsync(tripServiceCreateModel);

            carServiceModel.HireStatus = HireStatus.Unavailable;
            carServiceModel.ParkingId = null;
            carServiceModel.Parking = null;
            await this.carsService.EditAsync(carServiceModel);

            return this.Redirect("/Cars/All");
        }

        [Authorize]
        public async Task<IActionResult> MyTrips()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var trips = await this.tripsService.GetByUserAsync(currentUser.Id);

            var viewModel = new MyTripsAllViewModelCollection()
            {
                MyTrips = trips.Select(r => r.To<MyTripsAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Pay(int id, string secondId)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(secondId);
            var trip = await this.tripsService.GetByIdAsync<MyTripsServiceAllModel>(id);
            trip.CarId = car.Id;
            trip.Car = car.To<Car>();
            var viewModel = trip.To<MyTripsAllViewModel>();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost("Trips/Pay/{id}/{secondId}")]
        public async Task<IActionResult> Pay(MyTripsAllViewModel tripsAllViewModel)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            tripsAllViewModel.ClientId = currentUser.Id;
            var tripServiceModel = tripsAllViewModel.To<MyTripsServiceAllModel>();

            var points = await this.tripsService.PayAsync(tripServiceModel);

            await this.usersService.GetPointsAsync(currentUser.Id, points);

            return this.Redirect("/Trips/MyTrips");
        }
    }
}
