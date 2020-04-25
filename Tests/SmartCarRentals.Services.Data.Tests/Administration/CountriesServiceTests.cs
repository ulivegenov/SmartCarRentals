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
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Countries;

    using Xunit;

    public class CountriesServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task CreateAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var country = new CountryServiceInputModel() { Name = "Bulgaria" };

            var result = await countriesService.CreateAsync(country);
            var actualResult = await countriesRepository.All()
                                                        .Select(c => c.Name)
                                                        .FirstOrDefaultAsync();
            var expectedResult = country.Name;

            Assert.True(result == 1, ErrorMessage);
        }

        [Fact]
        public async Task EditAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var countryFromDb = await countriesRepository.All().FirstOrDefaultAsync();
            var country = countryFromDb.To<CountryServiceDetailsModel>();
            country.Name = "BGN";
            var result = await countriesService.EditAsync(country);

            Assert.True(result == 1, ErrorMessage);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var countries = await countriesService.GetAllAsync<CountriesServiceAllModel>();
            var count = countries.ToList().Count;

            Assert.True(count == 8, ErrorMessage);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldreturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var expectedCountryId = await countriesRepository.All()
                                                           .Where(c => c.Name == "Bulgaria")
                                                           .Select(c => c.Id)
                                                           .FirstOrDefaultAsync();
            var actualCountry = await countriesService.GetByIdAsync<CountryServiceDetailsModel>(expectedCountryId);

            Assert.True(expectedCountryId == actualCountry.Id, ErrorMessage);
        }

        [Fact]
        public async Task GetCountAsync_ShouldResturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            var count = await seeder.SeedCountriesAsync(context);

            var expectedCount = await countriesService.GetCountAsync();

            Assert.True(expectedCount == count, ErrorMessage);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var countryId = await countriesRepository.All()
                                                   .Where(c => c.Name == "Bulgaria")
                                                   .Select(c => c.Id)
                                                   .FirstOrDefaultAsync();

            var result = await countriesService.DeleteByIdAsync(countryId);

            Assert.True(result > 0, ErrorMessage);
        }

        [Fact]
        public async Task DeleteAllByIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var expectedIds = await countriesRepository.All()
                                                       .Select(c => c.Id)
                                                       .ToListAsync();
            var actulaIds = await countriesService.DeleteAllByIdAsync(expectedIds);
            var areEquivalent = (expectedIds.Count == actulaIds.ToList().Count) && !expectedIds.Except(actulaIds.ToList()).Any();

            Assert.True(areEquivalent, ErrorMessage);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 0)]
        [InlineData(3, 0)]
        [InlineData(4, 0)]
        [InlineData(5, 0)]
        public async Task GetAllWithPaging_ShouldReturnCorrectResult(int? take, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var countries = await countriesService.GetAllWithPagingAsync<CountryServiceDetailsModel>(take, skip);
            var countriesCount = countries.ToList().Count;

            Assert.True(take == countriesCount, ErrorMessage);
        }

        [Theory]
        [InlineData(null, 0)]
        [InlineData(10, 0)]
        public async Task GetAllWithPaging_ShouldReturnAllEntities(int? take, int skip)
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var countriesRepository = new EfDeletableEntityRepository<Country>(context);
            var countriesService = new CountriesService(countriesRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);

            var expectedCount = await countriesService.GetCountAsync();
            var countries = await countriesService.GetAllWithPagingAsync<CountryServiceDetailsModel>(take, skip);
            var countriesCount = countries.ToList().Count;

            Assert.True(expectedCount == countriesCount, ErrorMessage);
        }
    }
}
