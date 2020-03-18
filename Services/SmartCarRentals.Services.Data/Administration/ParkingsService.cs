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
        private const string InvalidParkingIdErrorMessage = "Parking with ID: {0} does not exist.";
        private const string InvalidParkingsIdsErrorMessage = "There is no Parking with any of these IDs.";

        private readonly IDeletableEntityRepository<Parking> parkingRepository;
        private readonly IDeletableEntityRepository<ParkingSlot> parkingSlotRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;

        public ParkingsService(
                               IDeletableEntityRepository<Parking> parkingRepository,
                               IDeletableEntityRepository<ParkingSlot> parkingSlotRepository,
                               IDeletableEntityRepository<Town> townRepository)
        {
            this.parkingRepository = parkingRepository;
            this.parkingSlotRepository = parkingSlotRepository;
            this.townRepository = townRepository;
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

        public async Task<int> DeleteByIdAsync(int id)
        {
            var parking = await this.parkingRepository.GetByIdWithDeletedAsync(id);

            if (parking == null)
            {
                throw new ArgumentNullException(string.Format(InvalidParkingIdErrorMessage, id));
            }

            this.parkingRepository.Delete(parking);
            await this.parkingRepository.SaveChangesAsync();

            return parking.Id;
        }

        public async Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids)
        {
            var parkings = await this.parkingRepository.All()
                                                       .Where(p => ids.Contains(p.Id))
                                                       .ToListAsync();

            if (parkings == null)
            {
                throw new ArgumentNullException(InvalidParkingsIdsErrorMessage);
            }

            foreach (var parking in parkings)
            {
                this.parkingRepository.Delete(parking);
            }

            await this.parkingRepository.SaveChangesAsync();

            var deletedParkingsIds = parkings.Select(p => p.Id).ToList();

            return deletedParkingsIds;
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

        public async Task<IEnumerable<Parking>> GetByTownIdAsync(int townId)
        {
            var parkings = await this.parkingRepository.All()
                                                       .Where(p => p.TownId == townId)
                                                       .ToListAsync();

            return parkings;
        }

        public async Task<IEnumerable<Parking>> GetAllByCountryIdAsync(int countryId)
        {
            var townsIds = await this.townRepository.All()
                                                    .Where(t => t.CountryId == countryId)
                                                    .Select(t => t.Id)
                                                    .ToListAsync();

            var countryParkings = await this.parkingRepository.All()
                                                       .Where(p => townsIds.Contains(p.TownId))
                                                       .ToListAsync();

            return countryParkings;
        }
    }
}
