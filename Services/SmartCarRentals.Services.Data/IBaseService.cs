namespace SmartCarRentals.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Contracts;

    public interface IBaseService<TKey>
    {
        Task<int> GetCountAsync();

        Task<int> CreateAsync(IServiceInputModel servicesInputViewModel);

        Task<IEnumerable<T>> GetAllAsync<T>();

        IEnumerable<T> GetAllWithPaging<T>(int? take = null, int skip = 0);

        Task<T> GetByIdAsync<T>(TKey id);

        Task<int> EditAsync(IServiceDetailsModel<TKey> serviceDetailsModel);

        Task<int> DeleteByIdAsync(TKey id);

        Task<IEnumerable<TKey>> DeleteAllByIdAsync(IEnumerable<TKey> ids);
    }
}
