namespace SmartCarRentals.Services.Data.Tests.Administration
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Repositories;
    using SmartCarRentals.Services.Data.Administration;
    using SmartCarRentals.Services.Data.Tests.Common;
    using SmartCarRentals.Services.Data.Tests.Common.Seeders;
    using SmartCarRentals.Services.Models.Administration.Cars;
    using Xunit;

    public class CarsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task GetCountByCountryAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetCountByCountryAsync("Bulgaria");

            Assert.True(result == 3, ErrorMessage);
        }

        [Fact]
        public async Task GetCountByTownAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetCountByTownAsync("Sofia");

            Assert.True(result == 3, ErrorMessage);
        }

        [Fact]
        public async Task GetCountByParkingAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetCountByParkingAsync("Sofia one");

            Assert.True(result == 3, ErrorMessage);
        }

        [Fact]
        public async Task GetAllByParkingAsync_ShouldReturnCorrectResult()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedParkingsAsync(context);
            await seeder.SeedCarsAsync(context);
            var result = await carsService.GetAllByParkingAsync<CarsServiceAllModel>(4);
            var count = result.ToList().Count;

            Assert.True(count == 3, ErrorMessage);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public async Task GetAllByCountryWithPagingAsync_ShouldReturnCorrectResult(int? take, int skip)
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetAllByCountryWithPagingAsync<CarsServiceAllModel>("Bulgaria", take, skip);

            Assert.True(result.Count() == take, ErrorMessage);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData(4, 0)]
        public async Task GetAllByCountryWithPagingAsync_ShouldReturnAllCarsByCountry(int? take, int skip)
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetAllByCountryWithPagingAsync<CarsServiceAllModel>("Bulgaria", take, skip);

            Assert.True(result.Count() == 3, ErrorMessage);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public async Task GetAllByTownWithPagingAsync_ShouldReturnCorrectResult(int? take, int skip)
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);
            var result = await carsService.GetAllByTownWithPagingAsync<CarsServiceAllModel>("Sofia", take, skip);

            Assert.True(result.Count() == take, ErrorMessage);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData(4, 0)]
        public async Task GetAllByTownWithPagingAsync_ShouldReturnAllCarsByTown(int? take, int skip)
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetAllByTownWithPagingAsync<CarsServiceAllModel>("Sofia", take, skip);

            Assert.True(result.Count() == 3, ErrorMessage);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        public async Task GetAllByParkingWithPagingAsync_ShouldReturnCorrectResult(int? take, int skip)
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetAllByParkingWithPagingAsync<CarsServiceAllModel>("Sofia one", take, skip);

            Assert.True(result.Count() == take, ErrorMessage);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData(4, 0)]
        public async Task GetAllByParkingWithPagingAsync_ShouldReturnAllCarsByTown(int? take, int skip)
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var result = await carsService.GetAllByParkingWithPagingAsync<CarsServiceAllModel>("Sofia one", take, skip);

            Assert.True(result.Count() == 3, ErrorMessage);
        }

        [Fact]
        public async Task IsCarAvailableByDate_ShouldReturnTrue_ForCarWithoutTripAndRaservation_ByDate()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var car = await carsRepository.All().FirstOrDefaultAsync();
            var result = await carsService.IsCarAvailableByDate(DateTime.UtcNow, car.Id);

            Assert.True(result, ErrorMessage);
        }

        [Fact]
        public async Task IsCarAvailableByDate_ShouldReturnFalse_ForCarWithTrip_WithoutRaservation_ByDate()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var car = await carsRepository.All().FirstOrDefaultAsync();
            var trip = new Trip() { CarId = car.Id };
            await tripsRepository.AddAsync(trip);
            await tripsRepository.SaveChangesAsync();
            var tripFromDbCarId = await tripsRepository.All().Select( t => t.CarId).FirstOrDefaultAsync();
            var result = await carsService.IsCarAvailableByDate(DateTime.UtcNow, tripFromDbCarId);

            Assert.False(result, ErrorMessage);
        }

        [Fact]
        public async Task IsCarAvailableByDate_ShouldReturnFalse_ForCarWithoutTrip_WithRaservation_ByDate()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var carsRepository = new EfDeletableEntityRepository<Car>(context);
            var tripsRepository = new EfDeletableEntityRepository<Trip>(context);
            var reservationsRepository = new EfDeletableEntityRepository<Reservation>(context);
            var parkingsRepository = new EfDeletableEntityRepository<Parking>(context);
            var parkingService = new ParkingsService(parkingsRepository);
            var carsService = new CarsService(carsRepository, tripsRepository, reservationsRepository, parkingService);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCarsAsync(context);

            var car = await carsRepository.All().FirstOrDefaultAsync();
            var date = DateTime.UtcNow.AddDays(5);
            var resevation = new Reservation() { ReservationDate = date, CarId = car.Id };
            await reservationsRepository.AddAsync(resevation);
            await reservationsRepository.SaveChangesAsync();
            var reservationFromDbCarId = await reservationsRepository.All().Select(r => r.CarId).FirstOrDefaultAsync();
            var result = await carsService.IsCarAvailableByDate(date, reservationFromDbCarId);

            Assert.False(result, ErrorMessage);
        }
    }
}
