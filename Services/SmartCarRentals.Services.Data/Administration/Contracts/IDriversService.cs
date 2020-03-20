namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Contracts;
    using SmartCarRentals.Services.Models.Drivers;

    public interface IDriversService
    {
        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<int> CreateAsync(IServiceInputModel servicesInputViewModel);

        Task<T> GetByIdAsync<T>(string id);

        Task<int> GetCountAsync();

        Task<int> EditAsync(IServiceDetailsModel<string> serviceDetailsModel);

        Task<int> DeleteByIdAsync(string id);
    }
}
