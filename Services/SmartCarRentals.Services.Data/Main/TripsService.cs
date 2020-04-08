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
    using SmartCarRentals.Data.Models.Enums.Trip;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contacts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.Trips;
    using SmartCarRentals.Web.ViewModels.Main.Trips;

    public class TripsService : BaseService<Trip, int>, ITripsService
    {
        private readonly IDeletableEntityRepository<Trip> tripsRepository;
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly ICarsService carsService;

        public TripsService(
                            IDeletableEntityRepository<Trip> tripsRepository,
                            IDeletableEntityRepository<Car> carRepository,
                            ICarsService carsService)
            : base(tripsRepository)
        {
            this.tripsRepository = tripsRepository;
            this.carRepository = carRepository;
            this.carsService = carsService;
        }

        public async Task<IEnumerable<MyTripsServiceAllModel>> GetByUserAsync(string userId)
        {
            var trips = await this.tripsRepository.All()
                                                  .Where(t => t.ClientId == userId)
                                                  .Select(t => new Trip()
                                                  {
                                                      Id = t.Id,
                                                      EndDate = t.EndDate,
                                                      KmRun = t.KmRun,
                                                      CarId = t.CarId,
                                                      Car = new Car()
                                                      {
                                                          Make = t.Car.Make,
                                                          Model = t.Car.Model,
                                                          ParkingId = t.Car.ParkingId,
                                                          PlateNumber = t.Car.PlateNumber,
                                                          PricePerDay = t.Car.PricePerDay,
                                                          PricePerHour = t.Car.PricePerHour,
                                                      },
                                                      HasPaid = t.HasPaid,
                                                      HasVote = t.HasVote,
                                                      Points = t.Points,
                                                      Price = t.Price,
                                                      Status = t.Status,
                                                  })
                                                  .To<MyTripsServiceAllModel>()
                                                  .ToListAsync();

            return trips;
        }

        public async Task<int> PayTrip(MyTripsServiceAllModel tripServiceModel)
        {
            tripServiceModel.EndDate = DateTime.UtcNow;

            var car = await this.carsService.GetByIdAsync<CarServiceDetailsModel>(tripServiceModel.CarId);

            decimal price = 0;

            if (tripServiceModel.EndDate.Value.Date == tripServiceModel.CreatedOn.Date)
            {
                var hours = tripServiceModel.EndDate.Value.Hour - tripServiceModel.CreatedOn.Hour;

                price = hours * car.PricePerHour;
            }
            else
            {
                var days = (tripServiceModel.EndDate.Value.Date - tripServiceModel.CreatedOn.Date).Days;

                price = days * car.PricePerDay;
            }

            tripServiceModel.Price = price;
            tripServiceModel.Points = (int)Math.Round(price / 10);
            tripServiceModel.Status = Status.Finished;
            tripServiceModel.HasPaid = true;

            var trip = tripServiceModel.To<Trip>();

            this.tripsRepository.Update(trip);

            await this.tripsRepository.SaveChangesAsync();

            return tripServiceModel.Points;
        }
    }
}
