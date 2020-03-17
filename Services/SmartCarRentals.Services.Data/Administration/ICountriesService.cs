namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Countries;

    public interface ICountriesService
    {
        Task<bool> CreateAsync(CountryServiceInputModel countryServicesInputViewModel);

        Task<bool> EditAsync(CountryServiceDetailsModel countryServiceDetailsModel);

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<CountriesServiceAllModel>> GetAllAsync();

        Task<Country> GetByNameAsync(string name);

        Task<CountryServiceDetailsModel> GetByIdAsync(int id);
    }
}
