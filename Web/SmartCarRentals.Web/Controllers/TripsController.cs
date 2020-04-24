namespace SmartCarRentals.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Trip;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.Trips;
    using SmartCarRentals.Web.ViewModels.Main.Trips;

    public class TripsController : BaseController
    {
        private const string UnavailableCarErrorMessage = "Sorry! This Car is unavailable, at the moment. Selecr some other!";

        private readonly ICarsService carsService;
        private readonly ITripsService tripsService;
        private readonly IUsersService usersService;
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TripsController(
                               ICarsService carsService,
                               ITripsService tripsService,
                               IUsersService usersService,
                               IReservationsService reservationsService,
                               UserManager<ApplicationUser> userManager)
        {
            this.carsService = carsService;
            this.tripsService = tripsService;
            this.usersService = usersService;
            this.reservationsService = reservationsService;
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
            if (tripCreateInputModel.CarId == null)
            {
                tripCreateInputModel.CarId = id;
                await this.reservationsService.SettingUpReservationStatusToAccomplished(id);
            }

            var currentUser = await this.userManager.GetUserAsync(this.User);
            tripCreateInputModel.ClientId = currentUser.Id;

            var isCarAvailableByDate = await this.carsService.IsCarAvailableByDate(DateTime.UtcNow, id);

            if (!this.ModelState.IsValid ||
                !isCarAvailableByDate)
            {
                var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
                tripCreateInputModel.Car = car.To<Car>();
                tripCreateInputModel.ClientId = currentUser.Id;

                if (!isCarAvailableByDate)
                {
                    this.TempData["Error"] = UnavailableCarErrorMessage;
                }

                return this.View(tripCreateInputModel);
            }

            var tripServiceCreateModel = tripCreateInputModel.To<TripServiceInputModel>();
            await this.tripsService.CreateAsync(tripServiceCreateModel);

            return this.Redirect("/Trips/MyTrips");
        }

        [Authorize]
        public async Task<IActionResult> MyTrips(int id = 1)
        {
            var page = id;
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var trips = await this.tripsService.GetByUserAsync(currentUser.Id, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var viewModel = new MyTripsAllViewModelCollection()
            {
                MyTrips = trips.Select(r => r.To<MyTripsAllViewModel>()).ToList(),
            };

            var count = await this.tripsService.GetCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost("Trips/MyTrips")]
        public async Task<IActionResult> FinishTrip(int id)
        {
            var trip = await this.tripsService.GetByIdAsync<MyTripsServiceAllModel>(id);
            trip.Status = Status.Finished;
            await this.tripsService.EditAsync(trip);

            return this.Redirect("/Trips/MyTrips");
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
