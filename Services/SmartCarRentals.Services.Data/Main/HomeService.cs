namespace SmartCarRentals.Services.Data.Main
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Cars;

    public class HomeService : IHomeService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;

        public HomeService(IDeletableEntityRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task<IEnumerable<CarsHotOffersServiceModel>> GetHotOffersCarsAsync()
        {
            var cars = await this.carRepository.All()
                                               .OrderByDescending(c => c.CreatedOn)
                                               .Select(c => new Car()
                                               {
                                                   Id = c.Id,
                                                   Make = c.Make,
                                                   Model = c.Model,
                                                   PricePerDay = c.PricePerDay,
                                                   Image = c.Image,
                                                   Class = c.Class,
                                                   Rating = c.Ratings.Count != 0
                                                            ? c.Ratings.Select(r => r.RatingVote).Average()
                                                            : 0,
                                               })
                                               .Take(4)
                                               .ToListAsync();

            var carsService = cars.Select(c => c.To<CarsHotOffersServiceModel>()).ToList();

            return carsService;
        }
    }
}
