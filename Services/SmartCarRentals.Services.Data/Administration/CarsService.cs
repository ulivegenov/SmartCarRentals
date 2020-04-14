namespace SmartCarRentals.Services.Data.Administration
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
    using SmartCarRentals.Services.Mapping;

    public class CarsService : BaseService<Car, string>, ICarsService
    {
        private const string InvalidCarIdErrorMessage = "{0} with ID: {1} does not exist.";
        private const string InvalidOperationErrorMessage = "Car with ID: {1} can't be deleted, because is currently in use.";

        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IDeletableEntityRepository<Trip> tripsRepository;
        private readonly IDeletableEntityRepository<Reservation> reservationsRepository;
        private readonly IParkingsService parkingsService;

        public CarsService(
                           IDeletableEntityRepository<Car> carsRepository,
                           IDeletableEntityRepository<Trip> tripsRepository,
                           IDeletableEntityRepository<Reservation> reservationsRepository,
                           IParkingsService parkingsService)
            : base(carsRepository)
        {
            this.carRepository = carsRepository;
            this.tripsRepository = tripsRepository;
            this.reservationsRepository = reservationsRepository;
            this.parkingsService = parkingsService;
        }

        public override async Task<int> DeleteByIdAsync(string id)
        {
            var car = await this.carRepository.All()
                                              .FirstOrDefaultAsync(c => c.Id == id);

            if (car == null)
            {
                throw new ArgumentNullException(string.Format(InvalidCarIdErrorMessage, id));
            }

            if (car.ReservationStatus == ReservationStatus.Reserved || car.ParkingId == null)
            {
                throw new InvalidOperationException(string.Format(InvalidOperationErrorMessage, id));
            }

            var carParking = await this.parkingsService.GetByCarIdAsync(id);
            carParking.Cars.Remove(car);

            this.carRepository.Delete(car);
            var result = await this.carRepository.SaveChangesAsync();

            return result;
        }

        public override async Task<T> GetByIdAsync<T>(string id)
        {
            var car = await this.carRepository.All()
                                              .Where(c => c.Id == id)
                                              .Select(c => new Car()
                                              {
                                                  Id = c.Id,
                                                  Image = c.Image,
                                                  Make = c.Make,
                                                  Model = c.Model,
                                                  Class = c.Class,
                                                  HireStatus = c.HireStatus,
                                                  Fuel = c.Fuel,
                                                  Transmition = c.Transmition,
                                                  KmRun = c.KmRun,
                                                  PricePerHour = c.PricePerHour,
                                                  PricePerDay = c.PricePerDay,
                                                  PlateNumber = c.PlateNumber,
                                                  Rating = c.Ratings.Count != 0
                                                           ? c.Ratings.Select(r => r.RatingVote).Average()
                                                           : 0,
                                                  Trips = c.Trips.Select(t => new Trip() { Id = t.Id, }).ToList(),
                                                  PassengersCapacity = c.PassengersCapacity,
                                                  ParkingId = c.ParkingId,
                                                  Parking = c.Parking,
                                              })
                                              .To<T>()
                                              .FirstOrDefaultAsync();

            return car;
        }

        public override async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var cars = await this.carRepository.All()
                                               .Select(c => new Car()
                                               {
                                                   Id = c.Id,
                                                   Image = c.Image,
                                                   Make = c.Make,
                                                   Model = c.Model,
                                                   Class = c.Class,
                                                   HireStatus = c.HireStatus,
                                                   PricePerDay = c.PricePerDay,
                                                   Rating = c.Ratings.Count != 0
                                                            ? c.Ratings.Select(r => r.RatingVote).Average()
                                                            : 0,
                                                   Trips = c.Trips.Select(t => new Trip() { Id = t.Id, }).ToList(),
                                                   ParkingId = c.ParkingId,
                                                   Parking = new Parking()
                                                   {
                                                       Name = c.Parking.Name,
                                                       Address = c.Parking.Address,
                                                       Town = new Town()
                                                       {
                                                           Name = c.Parking.Town.Name,
                                                           Country = new Country
                                                           {
                                                               Id = c.Parking.Town.CountryId,
                                                               Name = c.Parking.Town.Country.Name,
                                                           },
                                                       },
                                                   },
                                               })
                                               .To<T>()
                                               .ToListAsync();

            return cars;
        }

        public async Task<IEnumerable<T>> GetAllByParkingAsync<T>(int parkingId)
        {
            var cars = await this.carRepository.All()
                                               .Where(c => c.ParkingId == parkingId)
                                               .Select(c => new Car()
                                               {
                                                   Id = c.Id,
                                                   Image = c.Image,
                                                   Make = c.Make,
                                                   Model = c.Model,
                                                   Class = c.Class,
                                                   HireStatus = c.HireStatus,
                                                   PricePerDay = c.PricePerDay,
                                                   Rating = c.Ratings.Count != 0
                                                            ? c.Ratings.Select(r => r.RatingVote).Average()
                                                            : 0,
                                                   Trips = c.Trips.Select(t => new Trip() { Id = t.Id, }).ToList(),
                                                   ParkingId = c.ParkingId,
                                                   Parking = new Parking()
                                                   {
                                                       Name = c.Parking.Name,
                                                       Address = c.Parking.Address,
                                                       Town = new Town()
                                                       {
                                                           Name = c.Parking.Town.Name,
                                                           Country = new Country { Name = c.Parking.Town.Country.Name },
                                                       },
                                                   },
                                               })
                                               .To<T>()
                                               .ToListAsync();

            return cars;
        }

        public async Task<bool> IsCarAvailableByDate(DateTime date, string carId)
        {
            var trips = await this.tripsRepository.All()
                                                  .Where(t => t.CreatedOn.Date.CompareTo(date.Date) <= 0
                                                         && t.EndDate == null)
                                                  .Select(t => t.CarId)
                                                  .ToListAsync();

            var reservations = await this.reservationsRepository.All()
                                                                .Where(r => r.ReservationDate.Date.CompareTo(date.Date) == 0
                                                                       && r.Status != Status.Canceled)
                                                                .Select(r => r.CarId)
                                                                .ToListAsync();

            var cars = await this.carRepository.All()
                                               .Where(c => trips.Contains(c.Id)
                                                      || reservations.Contains(c.Id))
                                               .Select(c => c.Id)
                                               .ToListAsync();

            var result = !cars.Contains(carId);

            return result;
        }
    }
}
