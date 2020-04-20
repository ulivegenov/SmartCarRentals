namespace SmartCarRentals.Services.Data.Tests.Main
{
    using System.Linq;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Main;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;

    using Xunit;

    public class HomeServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task GetHotOffersCarsAsync_ShouldReturnFourCars()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carRepository = new EfDeletableEntityRepository<Car>(context);
            var homeService = new HomeService(carRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await homeService.GetHotOffersCarsAsync();

            Assert.True(result.Count() == 4, ErrorMessage);
        }

        [Fact]
        public async Task GetHotOffersCarsAsync_ShouldReturnLastFourCars()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carRepository = new EfDeletableEntityRepository<Car>(context);
            var homeService = new HomeService(carRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var hotOffersCars = await homeService.GetHotOffersCarsAsync();
            var result = hotOffersCars.Any(c => c.Make == "Opel");

            Assert.False(result, ErrorMessage);
        }
    }
}
