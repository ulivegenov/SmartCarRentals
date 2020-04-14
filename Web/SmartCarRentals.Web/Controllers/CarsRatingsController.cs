namespace SmartCarRentals.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.CarsRating;
    using SmartCarRentals.Services.Models.Main.Trips;
    using SmartCarRentals.Web.ViewModels.Main.CarsRatings;

    public class CarsRatingsController : BaseController
    {
        private readonly ICarsRatingsService carsRatingsService;
        private readonly ITripsService tripsService;
        private readonly ICarsService carsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CarsRatingsController(
                                     ICarsRatingsService carsRatingsService,
                                     ITripsService tripsService,
                                     ICarsService carsService,
                                     UserManager<ApplicationUser> userManager)
        {
            this.carsRatingsService = carsRatingsService;
            this.tripsService = tripsService;
            this.carsService = carsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create(int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var tripServiceModel = await this.tripsService.GetByIdAsync<MyTripsServiceAllModel>(id);
            var carServiceModel = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(tripServiceModel.CarId);
            var viewModel = new CarRatingCreateInputModel()
            {
                ClientId = currentUser.Id,
                TripId = tripServiceModel.Id,
                CarId = carServiceModel.Id,
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CarRatingCreateInputModel carRatingCreateInputModel, int id)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var trip = await this.tripsService.GetByIdAsync<MyTripsServiceAllModel>(id);
            var carId = trip.CarId;

            if (!this.ModelState.IsValid)
            {
                carRatingCreateInputModel.ClientId = currentUser.Id;
                carRatingCreateInputModel.TripId = trip.Id;
                carRatingCreateInputModel.CarId = carId;

                return this.View(carRatingCreateInputModel);
            }

            carRatingCreateInputModel.ClientId = currentUser.Id;
            carRatingCreateInputModel.TripId = trip.Id;
            carRatingCreateInputModel.CarId = carId;

            var carRatingServiceModel = carRatingCreateInputModel.To<CarRatingServiceInputModel>();

            await this.carsRatingsService.CreateAsync(carRatingServiceModel);

            await this.tripsService.VoteAsync(trip.Id);

            return this.Redirect("/Trips/MyTrips");
        }
    }
}
