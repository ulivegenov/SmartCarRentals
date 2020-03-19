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

        Task<int> DeleteByIdAsync(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<Country> GetByNameAsync(string name);

        Task<CountryServiceDetailsModel> GetByIdAsync(int id);
    }
}
