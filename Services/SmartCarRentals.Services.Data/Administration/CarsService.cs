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
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;

    public class CarsService : BaseService<Car, string>, ICarsService
    {
        private const string InvalidCarIdErrorMessage = "{0} with ID: {1} does not exist.";
        private const string InvalidOperationErrorMessage = "Car with ID: {1} can't be deleted, because is currently in use.";

        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly IParkingsService parkingsService;

        public CarsService(
                           IDeletableEntityRepository<Car> carRepository,
                           IParkingsService parkingsService)
            : base(carRepository)
        {
            this.carRepository = carRepository;
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
                                                          Country = new Country { Name = c.Parking.Town.Country.Name },
                                                      },
                                                  },
                                              })
                                              .To<T>()
                                              .ToListAsync();

            return cars;
        }
    }
}
