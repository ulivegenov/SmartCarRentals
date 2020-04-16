namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarsService : IBaseService<string>
    {
        Task<int> GetCountByCountryAsync(string countryName);

        Task<int> GetCountByTownAsync(string townName);

        Task<int> GetCountByParkingAsync(string parkingName);

        Task<IEnumerable<T>> GetAllByParkingAsync<T>(int parkingId);

        Task<IEnumerable<T>> GetAllByCountryWithPagingAsync<T>(string countryName, int? take = null, int skip = 0);

        Task<IEnumerable<T>> GetAllByTownWithPagingAsync<T>(string townName, int? take = null, int skip = 0);

        Task<IEnumerable<T>> GetAllByParkingWithPagingAsync<T>(string parkingId, int? take = null, int skip = 0);

        Task<bool> IsCarAvailableByDate(DateTime date, string carId);
    }
}
