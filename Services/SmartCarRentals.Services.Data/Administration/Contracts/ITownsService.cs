namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Contracts;

    public interface ITownsService
    {
        Task<int> CreateAsync(IServiceInputModel servicesInputViewModel);

        Task<int> EditAsync(IServiceDetailsModel<int> serviceDetailsModel);

        Task<int> DeleteByIdAsync(int id);

        Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids);

        Task<int> GetCountAsync();

        public Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId);
    }
}
