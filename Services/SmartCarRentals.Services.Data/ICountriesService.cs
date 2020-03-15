namespace SmartCarRentals.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Countries;

    public interface ICountriesService
    {
        Task<bool> CreateAsync(CountryServiceInputModel countryServicesInputViewModel);

        Task<bool> EditAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<CountriesServiceAllModel>> GetAllAsync();
    }
}
