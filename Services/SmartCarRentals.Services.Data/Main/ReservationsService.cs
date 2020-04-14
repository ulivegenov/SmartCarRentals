namespace SmartCarRentals.Services.Data.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.Reservations;

    public class ReservationsService : BaseService<Reservation, int>, IReservationsService
    {
        private readonly IDeletableEntityRepository<Reservation> reservationRepository;
        private readonly ICarsService carsService;

        public ReservationsService(
                                   IDeletableEntityRepository<Reservation> reservationRepository,
                                   ICarsService carsService)
            : base(reservationRepository)
        {
            this.reservationRepository = reservationRepository;
            this.carsService = carsService;
        }

        public async Task<IEnumerable<MyReservationsServiceAllModel>> GetByUserAsync(string userId, int? take = null, int skip = 0)
        {
            var reservations = this.reservationRepository.All()
                                                         .Where(r => r.ClientId == userId)
                                                         .Select(r => new Reservation()
                                                         {
                                                             Id = r.Id,
                                                             CarId = r.CarId,
                                                             Car = new Car()
                                                             {
                                                                 Make = r.Car.Make,
                                                                 Model = r.Car.Model,
                                                                 PlateNumber = r.Car.PlateNumber,
                                                             },
                                                             ReservationDate = r.ReservationDate,
                                                             Status = r.Status,
                                                             Parking = new Parking()
                                                             {
                                                                 Address = r.Parking.Address,
                                                                 Town = new Town()
                                                                 {
                                                                     Name = r.Parking.Town.Name,
                                                                 },
                                                             },
                                                         })
                                                         .OrderBy(r => r.Status)
                                                         .To<MyReservationsServiceAllModel>()
                                                         .Skip(skip);

            if (take.HasValue)
            {
                reservations = reservations.Take(take.Value);
            }

            return await reservations.ToListAsync();
        }

        public async Task<IEnumerable<MyReservationsServiceAllModel>> GetAllAwaitingReservationsAsync()
        {
            var resevations = await this.reservationRepository.All()
                                                              .Where(r => r.Status == Status.Awaiting)
                                                              .Select(r => new Reservation()
                                                              {
                                                                  Id = r.Id,
                                                                  Status = r.Status,
                                                                  ReservationDate = r.ReservationDate,
                                                                  CarId = r.CarId,
                                                                  ClientId = r.ClientId,
                                                                  ParkingId = r.ParkingId,
                                                              })
                                                              .OrderByDescending(r => r.ReservationDate)
                                                              .To<MyReservationsServiceAllModel>()
                                                              .ToListAsync();

            return resevations;
        }

        public async Task<int> CancelAsync(int reservationId)
        {
            var reservation = await this.reservationRepository.All()
                                                              .FirstOrDefaultAsync(r => r.Id == reservationId);

            reservation.Status = Status.Canceled;
            this.reservationRepository.Update(reservation);
            await this.reservationRepository.SaveChangesAsync();

            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(reservation.CarId);
            car.HireStatus = HireStatus.Available;
            car.ReservationStatus = ReservationStatus.Free;

            var result = await this.carsService.EditAsync(car);

            return result;
        }

        public async Task<int> SettingUpReservationStatusToAccomplished(string carId)
        {
            var reservation = await this.reservationRepository.All()
                                                              .FirstOrDefaultAsync(r => r.ReservationDate.Date.CompareTo(DateTime.UtcNow.Date) == 0
                                                                                   && r.CarId == carId);

            reservation.Status = Status.Accomplished;
            this.reservationRepository.Update(reservation);

            var result = await this.reservationRepository.SaveChangesAsync();

            return result;
        }
    }
}
