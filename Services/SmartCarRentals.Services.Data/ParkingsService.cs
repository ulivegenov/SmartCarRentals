namespace SmartCarRentals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;

    public class ParkingsService : IParkingsService
    {
        private readonly IDeletableEntityRepository<Parking> parkingRepository;

        public ParkingsService(IDeletableEntityRepository<Parking> parkingRepository)
        {
            this.parkingRepository = parkingRepository;
        }

        public Task<bool> CreateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParkingServiceModel>> GetAllAsync()
        {
            var parkings = await this.parkingRepository.AllAsNoTrackingWithDeleted()
                                                       .To<ParkingServiceModel>()
                                                       .ToListAsync();

            return parkings;
        }

        public async Task<int> GetCountAsync()
        {
            var parkings = await this.parkingRepository.AllAsNoTrackingWithDeleted().ToListAsync();
            var count = parkings.Count;

            return count;
        }
    }
}
