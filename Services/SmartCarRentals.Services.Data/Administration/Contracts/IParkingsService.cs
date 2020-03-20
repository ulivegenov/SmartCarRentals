namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Contracts;
    using SmartCarRentals.Services.Models.Parkings;

    public interface IParkingsService
    {
        Task<int> CreateAsync(IServiceInputModel servicesInputViewModel);

        Task<int> EditAsync(IServiceDetailsModel<int> serviceDetailsModel);

        Task<int> DeleteByIdAsync(int id);

        Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids);

        Task<int> GetCountAsync();

        Task<IEnumerable<T>> GetAllAsync<T>();

        public Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<Parking>> GetAllByTownIdAsync(int townId);

        Task<IEnumerable<Parking>> GetAllByCountryIdAsync(int countryId);
    }
}
