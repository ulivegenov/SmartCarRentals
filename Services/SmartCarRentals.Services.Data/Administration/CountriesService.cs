namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;

    public class CountriesService : ICountriesService
    {
        private readonly IDeletableEntityRepository<Country> countryRepository;

        public CountriesService(IDeletableEntityRepository<Country> countryRepository)
        {
            this.countryRepository = countryRepository;
        }

        public async Task<bool> CreateAsync(CountryServiceInputModel countryServicesInputViewModel)
        {
            var country = countryServicesInputViewModel.To<Country>();

            await this.countryRepository.AddAsync(country);
            var result = await this.countryRepository.SaveChangesAsync();

            return result > 0;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditAsync(CountryServiceDetailsModel countryServiceDetailsModel)
        {
            var country = countryServiceDetailsModel.To<Country>();

            this.countryRepository.Update(country);
            var result = await this.countryRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<CountriesServiceAllModel>> GetAllAsync()
        {
            var countries = await this.countryRepository.All()
                                                        .To<CountriesServiceAllModel>()
                                                        .ToListAsync();

            return countries;
        }

        public async Task<int> GetCountAsync()
        {
            var countries = await this.countryRepository.All().ToListAsync();
            var count = countries.Count;

            return count;
        }

        public async Task<Country> GetByNameAsync(string name) // TO DO MAPPING!!!
        {
            var country = await this.countryRepository.All().FirstOrDefaultAsync(c => c.Name == name);

            return country;
        }

        public async Task<CountryServiceDetailsModel> GetByIdAsync(int id)
        {
            var country = await this.countryRepository.All().FirstOrDefaultAsync(c => c.Id == id);
            var countryServiceModel = country.To<CountryServiceDetailsModel>();

            return countryServiceModel;
        }
    }
}
