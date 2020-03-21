namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;

    public class CountriesService : AdministrationService<Country, int>, ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CountriesService(IDeletableEntityRepository<Country> countryRepository)
            : base(countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public override async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var countries = await this.countryRepository.All()
                                                        .Select(c => new Country()
                                                        {
                                                            Id = c.Id,
                                                            Name = c.Name,
                                                            Towns = c.Towns.Select(t => new Town()
                                                            {
                                                                Parkings = t.Parkings,
                                                            })
                                                            .ToList(),
                                                        })
                                                        .To<T>()
                                                        .ToListAsync();

            return countries;
        }

        //public async Task<CountryServiceDetailsModel> GetByIdAsync(int id)
        //{
        //    var country = await this.countryRepository.All().FirstOrDefaultAsync(c => c.Id == id);
        //    var countryServiceModel = country.To<CountryServiceDetailsModel>();

        //    return countryServiceModel;
        //}
    }
}
