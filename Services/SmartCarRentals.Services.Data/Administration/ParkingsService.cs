namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.ParkoingLot;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;

    public class ParkingsService : IParkingsService
    {
        private readonly IDeletableEntityRepository<Parking> parkingRepository;
        private readonly IDeletableEntityRepository<ParkingSlot> parkingSlotRepository;

        public ParkingsService(
                               IDeletableEntityRepository<Parking> parkingRepository,
                               IDeletableEntityRepository<ParkingSlot> parkingSlotRepository)
        {
            this.parkingRepository = parkingRepository;
            this.parkingSlotRepository = parkingSlotRepository;
        }

        public async Task<bool> CreateAsync(ParkingServiceInputModel parkingServiceInputModel)
        {
            var parking = parkingServiceInputModel.To<Parking>();

            await this.parkingRepository.AddAsync(parking);
            var resultOne = await this.parkingRepository.SaveChangesAsync();

            var lastParking = await this.parkingRepository.All().FirstOrDefaultAsync(p => p.Name == parking.Name);

            for (int i = 0; i < lastParking.Capacity; i++)
            {
                var parkingSlot = new ParkingSlot
                {
                    Number = i,
                    Status = Status.Free,
                    ParkingId = lastParking.Id,
                };

                await this.parkingSlotRepository.AddAsync(parkingSlot);
            }

            var resultTwo = await this.parkingSlotRepository.SaveChangesAsync();
            var result = resultOne > 0 && resultTwo > 0;

            return result;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ParkingsServiceAllModel>> GetAllAsync()
        {
            var parkings = await this.parkingRepository.All()
                                                       .To<ParkingsServiceAllModel>()
                                                       .ToListAsync();

            return parkings;
        }

        public async Task<int> GetCountAsync()
        {
            var parkings = await this.parkingRepository.All().ToListAsync();
            var count = parkings.Count;

            return count;
        }
    }
}
