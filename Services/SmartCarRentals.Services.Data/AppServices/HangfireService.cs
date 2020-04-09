namespace SmartCarRentals.Services.Data.AppServices
{
    using System;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.AppServices.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;

    public class HangfireService : IHangfireService
    {
        private readonly ICarsService carsService;
        private readonly IReservationsService reservationsService;
        private readonly IDeletableEntityRepository<Car> carsRepository;
        private readonly IDeletableEntityRepository<Reservation> reservationsRepository;

        public HangfireService(
                               ICarsService carsService,
                               IReservationsService reservationsService,
                               IDeletableEntityRepository<Car> carsRepository,
                               IDeletableEntityRepository<Reservation> reservationsRepository)
        {
            this.carsService = carsService;
            this.reservationsService = reservationsService;
            this.carsRepository = carsRepository;
            this.reservationsRepository = reservationsRepository;
        }

        public async Task<int> CancelExpiredReservations()
        {
            var reservations = await this.reservationsService.GetAllAwaitingReservationsAsync();

            foreach (var reservation in reservations)
            {
                if (reservation.ReservationDate.CompareTo(DateTime.UtcNow) < 0 && reservation.Status != Status.Accomplished)
                {
                    reservation.Status = Status.Canceled;
                    this.reservationsRepository.Update(reservation.To<Reservation>());

                    var currentCar = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(reservation.CarId);
                    currentCar.HireStatus = HireStatus.Available;
                    currentCar.ReservationStatus = ReservationStatus.Free;
                    this.carsRepository.Update(currentCar.To<Car>());
                }
            }

            await this.reservationsRepository.SaveChangesAsync();
            var result = await this.carsRepository.SaveChangesAsync();

            return result;
        }
    }
}
