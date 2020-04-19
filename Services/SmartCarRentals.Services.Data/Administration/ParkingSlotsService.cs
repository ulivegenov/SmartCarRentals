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
    using SmartCarRentals.Services.Data.Administration.Contracts;

    public class ParkingSlotsService : BaseService<ParkingSlot, int>, IParkingSlotsService
    {
        private const string InvalidParkingSlotIdErrorMessage = "ParkingSlot with ID: {0} does not exist.";
        private const string InvalidParkingSlotsIdsErrorMessage = "There is no ParkingSlot with any of these IDs.";
        private const string OccupiedParkingSlotIdErrorMessage = "There is a Car on ParkingSlot with Number: {0}. You can not delete it while it is occupied!";

        private readonly IDeletableEntityRepository<ParkingSlot> parkingSlotRepository;
        private readonly IDeletableEntityRepository<Parking> parkingRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;

        public ParkingSlotsService(
                                   IDeletableEntityRepository<ParkingSlot> parkingSlotRepository,
                                   IDeletableEntityRepository<Parking> parkingRepository,
                                   IDeletableEntityRepository<Town> townRepository)
            : base(parkingSlotRepository)
        {
            this.parkingSlotRepository = parkingSlotRepository;
            this.parkingRepository = parkingRepository;
            this.townRepository = townRepository;
        }

        public override async Task<int> DeleteByIdAsync(int slotId)
        {
            var parkingSlot = await this.parkingSlotRepository.GetByIdWithDeletedAsync(slotId);

            if (parkingSlot == null)
            {
                throw new ArgumentNullException(string.Format(InvalidParkingSlotIdErrorMessage, slotId));
            }

            if (parkingSlot.Status != Status.Free)
            {
                throw new InvalidOperationException(string.Format(OccupiedParkingSlotIdErrorMessage, parkingSlot.Number));
            }

            this.parkingSlotRepository.Delete(parkingSlot);
            await this.parkingSlotRepository.SaveChangesAsync();

            return parkingSlot.Id;
        }

        public override async Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> slotIds)
        {
            var parkingSlots = await this.parkingSlotRepository.All()
                                                               .Where(ps => slotIds.Contains(ps.Id))
                                                               .ToListAsync();

            if (parkingSlots == null)
            {
                throw new ArgumentNullException(InvalidParkingSlotsIdsErrorMessage);
            }

            foreach (var parkingSlot in parkingSlots)
            {
                if (parkingSlot.Status != Status.Free)
                {
                    throw new InvalidOperationException(string.Format(OccupiedParkingSlotIdErrorMessage, parkingSlot.Number));
                }

                this.parkingSlotRepository.Delete(parkingSlot);
            }

            await this.parkingSlotRepository.SaveChangesAsync();

            var deletedSlotsIds = parkingSlots.Select(ps => ps.Id).ToList();

            return deletedSlotsIds;
        }

        public async Task<IEnumerable<ParkingSlot>> GetAllByParkingIdAsync(int parkingId)
        {
            var parkingSlots = await this.parkingSlotRepository.All()
                                                               .Where(ps => ps.ParkingId == parkingId)
                                                               .ToListAsync();

            return parkingSlots;
        }

        public async Task<IEnumerable<ParkingSlot>> GetAllByTownIdAsync(int townId)
        {
            var parkingsIds = await this.parkingRepository.All()
                                                          .Where(p => p.TownId == townId)
                                                          .Select(p => p.Id)
                                                          .ToListAsync();

            var parkingSlots = await this.parkingSlotRepository.All()
                                                               .Where(ps => parkingsIds.Contains(ps.ParkingId))
                                                               .ToListAsync();

            return parkingSlots;
        }

        public async Task<IEnumerable<ParkingSlot>> GetAllByCountryIdAsync(int countryId)
        {
            var townsIds = await this.townRepository.All()
                                                    .Where(t => t.CountryId == countryId)
                                                    .Select(t => t.Id)
                                                    .ToListAsync();

            var parkingsIds = await this.parkingRepository.All()
                                                          .Where(p => townsIds.Contains(p.TownId))
                                                          .Select(p => p.Id)
                                                          .ToListAsync();

            var parkingSlots = await this.parkingSlotRepository.All()
                                                               .Where(ps => parkingsIds.Contains(ps.ParkingId))
                                                               .ToListAsync();

            return parkingSlots;
        }
    }
}
