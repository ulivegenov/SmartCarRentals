namespace SmartCarRentals.Services.Data.Administration
{
    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CarsService : AdministrationService<Car, string>, ICarsService
    {
        private readonly IDeletableEntityRepository<Car> carRepository;

        public CarsService(IDeletableEntityRepository<Car> carRepository)
            : base(carRepository)
        {
            this.carRepository = carRepository;
        }

        //public override async Task<IEnumerable<T>> GetAllAsync<T>()
        //{
        //    var cars = await this.carRepository.All()
        //                                       .Select(c => new Car()
        //                                       {
        //                                           Id = c.Id,
        //                                           Make = c.Make,
        //                                           Model = c.Model,
        //                                           Class = c.Class,
        //                                           Rating = c.Rating,
        //                                           ParkingId = c.ParkingId,
        //                                           Trips = c.Trips.Select(t => new Trip() { Id = t.Id }).ToList(),
        //                                           Parking = c.Parking,
        //                                       })
        //                                       .To<T>
        //                                       .ToListAsync();
        //}
    }
}
