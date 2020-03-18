namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Towns;

    public interface ITownsService
    {
        Task<bool> CreateAsync(TownServiceInputModel countryServicesInputViewModel);

        Task<bool> EditAsync(int id);

        Task<int> DeleteByIdAsync(int id);

        Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids);

        Task<int> GetCountAsync();

        Town GetByName(string name);

        Task<IEnumerable<TownsServiceAllModel>> GetAllAsync();

        Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId);
    }
}
