namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.ParkoingLot;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;
    using SmartCarRentals.Services.Models.Contracts;

    public class ParkingsService : BaseService<Parking, int>, IParkingsService
    {
        private readonly IDeletableEntityRepository<Parking> parkingRepository;
        private readonly IDeletableEntityRepository<ParkingSlot> parkingSlotRepository;
        private readonly IDeletableEntityRepository<Town> townRepository;
        private readonly IDeletableEntityRepository<Car> carRepository;

        public ParkingsService(IDeletableEntityRepository<Parking> parkingRepository)
            : base(parkingRepository)
        {
            this.parkingRepository = parkingRepository;
        }

        public ParkingsService(
                               IDeletableEntityRepository<Parking> parkingRepository,
                               IDeletableEntityRepository<ParkingSlot> parkingSlotRepository,
                               IDeletableEntityRepository<Town> townRepository,
                               IDeletableEntityRepository<Car> carRepository)
            : this(parkingRepository)
        {
            this.parkingSlotRepository = parkingSlotRepository;
            this.townRepository = townRepository;
            this.carRepository = carRepository;
        }

        public override async Task<int> CreateAsync(IServiceInputModel servicesInputViewModel)
        {
            var parking = servicesInputViewModel.To<Parking>();

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

            return resultOne + resultTwo;
        }

        public override async Task<int> EditAsync(IServiceDetailsModel<int> serviceDetailsModel)
        {
            var parkingFromDb = await this.parkingRepository.All()
                                                       .Where(p => p.Id == serviceDetailsModel.Id)
                                                       .Select(p => new Parking
                                                       {
                                                           TownId = p.TownId,
                                                           Capacity = p.Capacity,
                                                           FreeParkingSlots = p.FreeParkingSlots,
                                                           ParkingSlots = p.ParkingSlots.Select(ps => new ParkingSlot()
                                                           {
                                                               Id = ps.Id,
                                                           })
                                                           .ToList(),
                                                           Cars = p.Cars.Select(c => new Car()
                                                           {
                                                               Id = c.Id,
                                                           })
                                                           .ToList(),
                                                           Reservations = p.Reservations.Select(r => new Reservation()
                                                           {
                                                               Id = r.Id,
                                                           })
                                                           .ToList(),
                                                       })
                                                       .FirstOrDefaultAsync();

            var parking = serviceDetailsModel.To<Parking>();
            parking.TownId = parkingFromDb.TownId;
            parking.Capacity = parkingFromDb.Capacity;
            parking.FreeParkingSlots = parkingFromDb.FreeParkingSlots;
            parking.ParkingSlots = parkingFromDb.ParkingSlots;
            parking.Cars = parkingFromDb.Cars;
            parking.Reservations = parkingFromDb.Reservations;

            this.parkingRepository.Update(parking);
            var result = await this.parkingRepository.SaveChangesAsync();

            return result;
        }

        public override async Task<T> GetByIdAsync<T>(int id)
        {
            var parking = await this.parkingRepository.All()
                                                       .Where(p => p.Id == id)
                                                       .Select(p => new Parking
                                                       {
                                                           Id = p.Id,
                                                           Name = p.Name,
                                                           Town = p.Town,
                                                           Address = p.Address,
                                                           Capacity = p.Capacity,
                                                           FreeParkingSlots = p.FreeParkingSlots,
                                                           Cars = p.Cars,
                                                           Reservations = p.Reservations,
                                                       })
                                                       .FirstOrDefaultAsync();

            var parkingServiceModel = parking.To<T>();

            return parkingServiceModel;
        }

        public async Task<IEnumerable<ParkingsServiceAllModel>> GetAllByTownIdAsync(int townId)
        {
            var parkings = await this.GetAllAsync<ParkingsServiceAllModel>();
            var townParkings = parkings.Where(p => p.Town.Id == townId).ToList();

            return townParkings;
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

        public async Task<Parking> GetByCarIdAsync(string id)
        {
            var car = await this.carRepository.All()
                                              .Where(c => c.Id == id)
                                              .Select(c => new Car()
                                              {
                                                  ParkingId = c.ParkingId,
                                              })
                                              .FirstOrDefaultAsync();

            var parking = await this.parkingRepository.All()
                                                      .Where(p => p.Id == car.ParkingId)
                                                      .Select(p => new Parking()
                                                      {
                                                          Id = p.Id,
                                                          Name = p.Name,
                                                          Address = p.Address,
                                                          Town = p.Town,
                                                      })
                                                      .FirstOrDefaultAsync();

            return parking;
        }
    }
}
