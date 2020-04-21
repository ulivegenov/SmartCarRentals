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

    public class ReservationsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Theory]
        [InlineData("abc", 3, 0)]
        [InlineData("abc", 2, 0)]
        [InlineData("abc", 1, 0)]
        public async Task GetByUserAsync_ShouldReturnCorrectReservationsCountForUserByPage(string clientId, int? reservationsPerPage, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = new ReservationsService(reservationRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedReservationsAsync(context);

            var result = await reservationsService.GetByUserAsync(clientId, reservationsPerPage, skip);

            Assert.True(result.ToList().Count == reservationsPerPage, ErrorMessage);
        }

        [Theory]
        [InlineData("abc", null, 0)]
        [InlineData("abc", 5, 0)]
        public async Task GetByUserAsync_ShouldReturnAllReservationsForUser(string clientId, int? reservationsPerPage, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = new ReservationsService(reservationRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedReservationsAsync(context);

            var result = await reservationsService.GetByUserAsync(clientId, reservationsPerPage, skip);

            Assert.True(result.ToList().Count == 3, ErrorMessage);
        }

        [Fact]
        public async Task GetAllAwaitingReservationsAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = new ReservationsService(reservationRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedReservationsAsync(context);

            var result = await reservationsService.GetAllAwaitingReservationsAsync();

            Assert.True(result.ToList().Count == 8, ErrorMessage);
        }

        [Fact]
        public async Task SettingUpReservationStatusToAccomplished_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = new ReservationsService(reservationRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedReservationsAsync(context);

            var result = await reservationsService.SettingUpReservationStatusToAccomplished("cde");

            Assert.True(result == 1, ErrorMessage);
        }

        [Fact]
        public async Task CancelAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var reservationRepository = new EfDeletableEntityRepository<Reservation>(context);
            var reservationsService = new ReservationsService(reservationRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedReservationsAsync(context);

            var result = await reservationsService.CancelAsync(1);

            Assert.True(result == 1, ErrorMessage);
        }
    }
}
