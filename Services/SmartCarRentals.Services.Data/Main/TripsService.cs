namespace SmartCarRentals.Services.Data.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Trip;
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using SmartCarRentals.Services.Models.Main.Trips;

    public class TripsService : BaseService<Trip, int>, ITripsService
    {
        private readonly IDeletableEntityRepository<Trip> tripsRepository;
        private readonly IDeletableEntityRepository<Car> carRepository;
        private readonly ICarsService carsService;
        private readonly UserManager<ApplicationUser> userManager;

        public TripsService(
                            IDeletableEntityRepository<Trip> tripsRepository,
                            IDeletableEntityRepository<Car> carRepository,
                            ICarsService carsService,
                            UserManager<ApplicationUser> userManager)
            : base(tripsRepository)
        {
            this.tripsRepository = tripsRepository;
            this.carRepository = carRepository;
            this.carsService = carsService;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<MyTripsServiceAllModel>> GetByUserAsync(string userId, int? take = null, int skip = 0)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var discount = GlobalConstants.UserDiscount;

            if (user.Rank == RankType.GoldUser)
            {
                discount = GlobalConstants.GoldUserDiscount;
            }

            if (user.Rank == RankType.PlatinumUser)
            {
                discount = GlobalConstants.PlatinumUserDiscount;
            }

            var trips = this.tripsRepository.All()
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
                                                    HireStatus = t.Car.HireStatus,
                                                    ParkingId = t.Car.ParkingId,
                                                    PlateNumber = t.Car.PlateNumber,
                                                    PricePerDay = t.Car.PricePerDay,
                                                    PricePerHour = t.Car.PricePerHour,
                                                },
                                                HasPaid = t.HasPaid,
                                                HasVote = t.HasVote,
                                                Points = t.Points,
                                                Price = t.Price - (t.Price * discount / 100),
                                                Status = t.Status,
                                            })
                                            .OrderBy(t => t.Status)
                                            .To<MyTripsServiceAllModel>()
                                            .Skip(skip);

            if (take.HasValue)
            {
                trips = trips.Take(take.Value);
            }

            return await trips.ToListAsync();
        }

        public async Task<int> PayAsync(MyTripsServiceAllModel tripServiceModel)
        {
            var userId = tripServiceModel.ClientId;
            var user = await this.userManager.FindByIdAsync(userId);
            var discount = GlobalConstants.UserDiscount;

            if (user.Rank == RankType.GoldUser)
            {
                discount = GlobalConstants.GoldUserDiscount;
            }

            if (user.Rank == RankType.PlatinumUser)
            {
                discount = GlobalConstants.PlatinumUserDiscount;
            }

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

            tripServiceModel.Price = price - (price * discount / 100);
            tripServiceModel.Points = (int)Math.Round(price / 10);
            tripServiceModel.Status = Status.Finished;
            tripServiceModel.HasPaid = true;

            var trip = tripServiceModel.To<Trip>();

            this.tripsRepository.Update(trip);

            await this.tripsRepository.SaveChangesAsync();

            return tripServiceModel.Points;
        }

        public async Task<int> VoteAsync(int triprId)
        {
            var trip = await this.tripsRepository.GetByIdWithDeletedAsync(triprId);

            trip.HasVote = true;
            var result = await this.tripsRepository.SaveChangesAsync();

            return result;
        }
    }
}
