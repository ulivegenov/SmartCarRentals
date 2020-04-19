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

    public class TownsServiceTests
    {
        private const string ErrorMessage = "Method does not work properly!";

        [Fact]
        public async Task GetAllByCountryIdAsync_ShouldReturnCorrectResult()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var townRepository = new EfDeletableEntityRepository<Town>(context);
            var countryRepository = new EfDeletableEntityRepository<Country>(context);
            var townsService = new TownsService(townRepository);
            var seeder = new DbContextTestsSeeder();
            await seeder.SeedCountriesAsync(context);
            await seeder.SeedTownsAsync(context);

            var countryId = await countryRepository.All()
                                                   .Select(c => c.Id)
                                                   .FirstOrDefaultAsync();

            var result = await townsService.GetAllByCountryIdAsync(countryId);

            Assert.True(result.ToList().Count == 1, ErrorMessage);
        }
    }
}
