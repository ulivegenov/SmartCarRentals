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

    public class ParkingSlotsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task GetAllByParkingIdAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var townRepository = new EfDeletableEntityRepository<Town>(context);
            var parkingSlotRepository = new EfDeletableEntityRepository<ParkingSlot>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingSlotsService = new ParkingSlotsService(parkingSlotRepository, parkingsRepository, townRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);
            await seeder.SeedParkingSlotsAsync(context);

            var parkingId = await parkingsRepository.All().Select(p => p.Id).FirstOrDefaultAsync();

            var result = await parkingSlotsService.GetAllByParkingIdAsync(parkingId);

            Assert.True(result.Count() == 4, ErrorMessage);
        }

        [Fact]
        public async Task GetAllByTownIdAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var townRepository = new EfDeletableEntityRepository<Town>(context);
            var parkingSlotRepository = new EfDeletableEntityRepository<ParkingSlot>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingSlotsService = new ParkingSlotsService(parkingSlotRepository, parkingsRepository, townRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);
            await seeder.SeedParkingSlotsAsync(context);

            var townId = await parkingsRepository.All().Select(p => p.TownId).FirstOrDefaultAsync();

            var result = await parkingSlotsService.GetAllByTownIdAsync(townId);

            Assert.True(result.Count() == 4, ErrorMessage);
        }

        [Fact]
        public async Task GetAllByCountryIdAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var townRepository = new EfDeletableEntityRepository<Town>(context);
            var parkingSlotRepository = new EfDeletableEntityRepository<ParkingSlot>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingSlotsService = new ParkingSlotsService(parkingSlotRepository, parkingsRepository, townRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);
            await seeder.SeedParkingSlotsAsync(context);
            await seeder.SeedTownsAsync(context);

            var countryId = await parkingsRepository.All().Select(p => p.Town.CountryId).FirstOrDefaultAsync();

            var result = await parkingSlotsService.GetAllByCountryIdAsync(countryId);

            Assert.True(result.Count() == 4, ErrorMessage);
        }
    }
}
