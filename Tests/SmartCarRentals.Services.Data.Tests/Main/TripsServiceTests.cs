namespace SmartCarRentals.Services.Data.Tests.Main
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Main;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Trips;
    using Xunit;

    public class TripsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Theory]
        [InlineData("abc", 2, 0)]
        [InlineData("abc", 1, 0)]
        public async Task GetByUserAsync_ShouldReturnCorrectReservationsCountForUserByPage(string clientId, int? tripsPerPage, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var result = await tripsService.GetByUserAsync(clientId, tripsPerPage, skip);

            Assert.True(result.ToList().Count == tripsPerPage, ErrorMessage);
        }

        [Theory]
        [InlineData("abc", null, 0)]
        [InlineData("abc", 5, 0)]
        public async Task GetByUserAsync_ShouldReturnAllReservationsForUser(string clientId, int? tripsPerPage, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var result = await tripsService.GetByUserAsync(clientId, tripsPerPage, skip);

            Assert.True(result.ToList().Count == 2, ErrorMessage);
        }

        [Fact]
        public async Task VoteAsync_ShouldRetutnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);

            var result = await tripsService.VoteAsync(1);

            Assert.True(result == 1, ErrorMessage);
        }

        [Fact]
        public async Task PayAsync_ShouldRetutnCorrectResultForDays_RankUser()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var trip = await tripsRepository.All().Where(t => t.Id == 2).FirstOrDefaultAsync();
            var tripsServiceModel = trip.To<MyTripsServiceAllModel>();
            var result = await tripsService.PayAsync(tripsServiceModel);

            Assert.True(result == 6, ErrorMessage);
        }

        [Fact]
        public async Task PayAsync_ShouldRetutnCorrectResultForDays_RankGoldUser()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var trip = await tripsRepository.All().Where(t => t.Id == 6).FirstOrDefaultAsync();
            var tripsServiceModel = trip.To<MyTripsServiceAllModel>();
            var result = await tripsService.PayAsync(tripsServiceModel);

            Assert.True(result == 6, ErrorMessage);
        }

        [Fact]
        public async Task PayAsync_ShouldRetutnCorrectResultForDays_RankPlatinumUser()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var trip = await tripsRepository.All().Where(t => t.Id == 7).FirstOrDefaultAsync();
            var tripsServiceModel = trip.To<MyTripsServiceAllModel>();
            var result = await tripsService.PayAsync(tripsServiceModel);

            Assert.True(result == 5, ErrorMessage);
        }

        [Fact]
        public async Task PayAsync_ShouldRetutnCorrectResultForHours_RankUser()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var trip = await tripsRepository.All().Where(t => t.Id == 5).FirstOrDefaultAsync();
            var tripsServiceModel = trip.To<MyTripsServiceAllModel>();
            var result = await tripsService.PayAsync(tripsServiceModel);

            Assert.True(result == 2, ErrorMessage);
        }

        [Fact]
        public async Task PayAsync_ShouldRetutnCorrectResultForHours_RankGoldUser()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var trip = await tripsRepository.All().Where(t => t.Id == 8).FirstOrDefaultAsync();
            var tripsServiceModel = trip.To<MyTripsServiceAllModel>();
            var result = await tripsService.PayAsync(tripsServiceModel);

            Assert.True(result == 2, ErrorMessage);
        }

        [Fact]
        public async Task PayAsync_ShouldRetutnCorrectResultForHours_RankPlatinumUser()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(context);
            var tripsService = new TripsService(tripsRepository, usersRepository, carsRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedTripsAsync(context);
            await seeder.SeedUsersAsync(context);

            var trip = await tripsRepository.All().Where(t => t.Id == 9).FirstOrDefaultAsync();
            var tripsServiceModel = trip.To<MyTripsServiceAllModel>();
            var result = await tripsService.PayAsync(tripsServiceModel);

            Assert.True(result == 2, ErrorMessage);
        }
    }
}
