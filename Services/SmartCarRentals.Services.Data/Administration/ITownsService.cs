namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Towns;

    public interface ITownsService
    {
        Task<bool> CreateAsync(TownServiceInputModel countryServicesInputViewModel);

        Task<bool> EditAsync(TownServiceDetailsModel townServiceDetailsModel);

        Task<int> DeleteByIdAsync(int id);

        Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids);

        Task<int> GetCountAsync();

        Town GetByName(string name);

        Task<TownServiceDetailsModel> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId);
    }
}
