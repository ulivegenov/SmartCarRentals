namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task<int> DeleteByIdAsync(string id)
        {
            var driver = await this.driverRepository.GetByIdWithDeletedAsync(id);
            this.driverRepository.Delete(driver);

            var result = await this.driverRepository.SaveChangesAsync();

            return result;
        }

        public async Task<int> EditAsync(DriverServiceDetailsModel driverServiceDetailsModel)
        {
            var driver = driverServiceDetailsModel.To<Driver>();

            this.driverRepository.Update(driver);

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

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var driver = await this.driverRepository.All()
                                                    .Where(d => d.Id == id)
                                                    .Select(d => new Driver()
                                                    {
                                                        Id = d.Id,
                                                        FirstName = d.FirstName,
                                                        LastName = d.LastName,
                                                        ImageUrl = d.ImageUrl,
                                                        Rating = d.Ratings.Select(r => r.RatingVote).Average(),
                                                        Transfers = d.Transfers.Select(t => new Transfer()
                                                        {
                                                            Id = t.Id,
                                                        })
                                                        .ToList(),
                                                    })
                                                    .To<T>()
                                                    .FirstOrDefaultAsync();

            return driver;
        }

        public async Task<int> GetCountAsync()
        {
            var drivers = await this.driverRepository.All().ToListAsync();
            var count = drivers.Count;

            return count;
        }
    }
}
