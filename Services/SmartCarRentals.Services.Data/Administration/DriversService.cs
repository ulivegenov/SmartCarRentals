namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;

    public class DriversService : IDriversService
    {
        private readonly IDeletableEntityRepository<Driver> driverRepository;

        public DriversService(IDeletableEntityRepository<Driver> driverRepository)
        {
            this.driverRepository = driverRepository;
        }

        public async Task<int> CreateAsync(DriverServiceInputModel driverServiceModel)
        {
            var driver = driverServiceModel.To<Driver>();
            await this.driverRepository.AddAsync(driver);

            var result = await this.driverRepository.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var drivers = await this.driverRepository.All()
                                                       .To<T>()
                                                       .ToListAsync();

            return drivers;
        }
    }
}
