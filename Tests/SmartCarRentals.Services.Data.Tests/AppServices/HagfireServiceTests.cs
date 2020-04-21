namespace SmartCarRentals.Services.Data.Tests.AppServices
{
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.AppServices;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;

    using Xunit;

    public class HagfireServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task CancelExpiredReservationsAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedReservationsAsync(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var transfersRepository = new EfDeletableEntityRepository<Transfer>(context);
            var hangfireService = new HangfireService(reservationsRepository, transfersRepository);

            var result = await hangfireService.CancelExpiredReservationsAsync();

            Assert.True(result == 2, ErrorMessage);
        }

        [Fact]
        public async Task SettingUpTransfersStatusByDateAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTransfersAsync(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var transfersRepository = new EfDeletableEntityRepository<Transfer>(context);

            var hangfireService = new HangfireService(reservationsRepository, transfersRepository);

            var result = await hangfireService.SettingUpTransfersStatusByDateAsync();

            Assert.True(result == 2, ErrorMessage);
        }
    }
}
