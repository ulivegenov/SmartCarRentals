namespace SmartCarRentals.Services.Data.Tests.Main
{
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Main;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;

    using Xunit;

    public class DriversRatingsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task DeleteAllByDriverIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var driversRatingRepository = new EfDeletableEntityRepository<DriverRating>(context);
            var driversRatingsService = new DriversRatingsService(driversRatingRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedDriversRatings(context);

            var result = await driversRatingsService.DeleteAllByDriverIdAsync("abc");

            Assert.True(result == 3, ErrorMessage);
        }

        [Theory]
        [InlineData(1, "abc")]
        [InlineData(2, "abc")]
        [InlineData(3, "abc")]
        [InlineData(4, "bcd")]
        [InlineData(5, "bcd")]
        [InlineData(6, "cde")]
        public async Task GetByTransferId_ShouldReturnCorrectresult(int transferId, string driverId)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var driversRatingRepository = new EfDeletableEntityRepository<DriverRating>(context);
            var driversRatingsService = new DriversRatingsService(driversRatingRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedDriversRatings(context);

            var result = await driversRatingsService.GetByTransferId(transferId);

            Assert.True(result.DriverId == driverId, ErrorMessage);
        }
    }
}
