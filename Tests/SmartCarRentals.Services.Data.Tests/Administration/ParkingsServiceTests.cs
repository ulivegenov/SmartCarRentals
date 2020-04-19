namespace SmartCarRentals.Services.Data.Tests.Administration
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;

    using Xunit;

    public class ParkingsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task GetAllByTownIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);

            var townId = await parkingsRepository.All()
                                                 .Select(p => p.TownId)
                                                 .FirstOrDefaultAsync();

            var result = await parkingService.GetAllByTownIdAsync(townId);

            Assert.True(result.ToList().Count == 1, ErrorMessage);
        }

        [Fact]
        public async Task GetAllByCountryIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var townRepository = new EfDeletableEntityRepository<Town>(context);
            var parkingSlotRepository = new EfDeletableEntityRepository<ParkingSlot>(context);
            var parkingRepository = new EfDeletableEntityRepository<Parking>(context);
            var carRepository = new EfDeletableEntityRepository<Car>(context);
            var parkingsService = new ParkingsService(parkingRepository, parkingSlotRepository, townRepository, carRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);
            await seeder.SeedTownsAsync(context);

            var countryId = await parkingRepository.All()
                                                 .Select(p => p.Town.CountryId)
                                                 .FirstOrDefaultAsync();

            var result = await parkingsService.GetAllByCountryIdAsync(countryId);

            Assert.True(result.ToList().Count == 1, ErrorMessage);
        }

        [Fact]
        public async Task GetByCarIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var townRepository = new EfDeletableEntityRepository<Town>(context);
            var parkingSlotRepository = new EfDeletableEntityRepository<ParkingSlot>(context);
            var parkingRepository = new EfDeletableEntityRepository<Parking>(context);
            var carRepository = new EfDeletableEntityRepository<Car>(context);
            var parkingsService = new ParkingsService(parkingRepository,parkingSlotRepository, townRepository, carRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);
            await seeder.SeedCarsAsync(context);

            var car = await carRepository.All().FirstOrDefaultAsync();

            var result = await parkingsService.GetByCarIdAsync(car.Id);

            Assert.True(result.Id == car.ParkingId, ErrorMessage);
        }
    }
}
