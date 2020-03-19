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
        private const string InvalidCountryIdErrorMessage = "Country with ID: {0} does not exist.";

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

        public async Task<int> DeleteByIdAsync(int id)
        {
            var country = await this.countryRepository.GetByIdWithDeletedAsync(id);

            if (country == null)
            {
                throw new ArgumentNullException(
                    string.Format(InvalidCountryIdErrorMessage, country.Id));
            }

            this.countryRepository.Delete(country);
            await this.countryRepository.SaveChangesAsync();

            return country.Id;
        }

        public async Task<bool> EditAsync(CountryServiceDetailsModel countryServiceDetailsModel)
        {
            var country = countryServiceDetailsModel.To<Country>();

            this.countryRepository.Update(country);
            var result = await this.countryRepository.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var countries = await this.countryRepository.All()
                                                        .To<T>()
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
