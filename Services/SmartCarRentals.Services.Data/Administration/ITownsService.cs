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

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountAsync();

        Town GetByName(string name);

        Task<IEnumerable<TownsServiceAllModel>> GetAllAsync();
    }
}
