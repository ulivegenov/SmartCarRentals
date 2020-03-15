namespace SmartCarRentals.Services.Data
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

        public Task<bool> EditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CountriesServiceAllModel>> GetAllAsync()
        {
            var countries = await this.countryRepository.All()
                                                        .To<CountriesServiceAllModel>()
                                                        .ToListAsync();

            return countries;
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
