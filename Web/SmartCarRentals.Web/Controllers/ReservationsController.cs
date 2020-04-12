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
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.Reservations;
    using SmartCarRentals.Web.ViewModels.Main.Reservations;

    public class ReservationsController : BaseController
    {
        private const string UnavailableCarErrorMessage = "Sorry! This Car is unavailable, at the moment.";
        private const string ReservationErrorMessage = "Sorry! This Car is already reserved for this date";

        private readonly ICarsService carsService;
        private readonly IReservationsService reservationsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReservationsController(
                                      ICarsService carsService,
                                      IReservationsService reservationsService,
                                      UserManager<ApplicationUser> userManager)
        {
            this.carsService = carsService;
            this.reservationsService = reservationsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Create(string id)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);

            var viewModel = new ReservationCreateInputModel()
            {
                CarId = id,
                Car = car.To<Car>(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(ReservationCreateInputModel reservationCreateInputModel, string id)
        {
            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(id);
            var currentUser = await this.userManager.GetUserAsync(this.User);

            reservationCreateInputModel.ClientId = currentUser.Id;
            reservationCreateInputModel.ParkingId = (int)car.ParkingId;

            if (car.ParkingId == null)
            {
                this.TempData["Error"] = UnavailableCarErrorMessage;

                return this.View(); // TODO ERROR MESSAGE VIEW!!!
            }

            if (!this.ModelState.IsValid)
            {
                reservationCreateInputModel.CarId = id;
                reservationCreateInputModel.Car = car.To<Car>();
                reservationCreateInputModel.ClientId = currentUser.Id;
                reservationCreateInputModel.ParkingId = (int)car.ParkingId;

                return this.View(reservationCreateInputModel);
            }

            var reservations = await this.reservationsService.GetAllAsync<ReservationServiceDetailsModel>();
            var possibleReservations = reservations.Where(r => r.CarId == reservationCreateInputModel.CarId &&
                                                       r.ReservationDate == reservationCreateInputModel.ReservationDate).ToList();

            if (possibleReservations != null)
            {
                if (possibleReservations.Any(r => r.Status != Status.Canceled))
                {
                    this.TempData["Error"] = ReservationErrorMessage;

                    reservationCreateInputModel.CarId = id;
                    reservationCreateInputModel.Car = car.To<Car>();
                    reservationCreateInputModel.ClientId = currentUser.Id;
                    reservationCreateInputModel.ParkingId = (int)car.ParkingId;

                    return this.View(reservationCreateInputModel); // TODO ERROR MESSAGE VIEW!!!
                }
            }

            var reservationServiceModel = reservationCreateInputModel.To<ReservationServiceInputModel>();
            await this.reservationsService.CreateAsync(reservationServiceModel);

            return this.Redirect("/Reservations/MyReservations");
        }

        [Authorize]
        public async Task<IActionResult> MyReservations(int id = 1)
        {
            var page = id;
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var reservations = await this.reservationsService.GetByUserAsync(currentUser.Id, GlobalConstants.ItemsPerPage, (page - 1) * GlobalConstants.ItemsPerPage);

            var viewModel = new MyReservationsAllViewModelCollection()
            {
                MyReservations = reservations.Select(r => r.To<MyReservationsAllViewModel>()).ToList(),
            };

            var count = await this.reservationsService.GetCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.UsersPerPageAdmin);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var reservation = await this.reservationsService.GetByIdAsync<ReservationServiceDetailsModel>(id);
            reservation.Status = Status.Canceled;

            await this.reservationsService.EditAsync(reservation);

            return this.Redirect("/Reservations/MyReservations");
        }
    }
}
