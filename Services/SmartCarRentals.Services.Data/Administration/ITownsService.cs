namespace SmartCarRentals.Services.Data.Administration
{
    using SmartCarRentals.Services.Models.Towns;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ITownsService
    {
        Task<bool> CreateAsync(TownServiceInputModel countryServicesInputViewModel);

        Task<bool> EditAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<TownsServiceAllModel>> GetAllAsync();
    }
}
