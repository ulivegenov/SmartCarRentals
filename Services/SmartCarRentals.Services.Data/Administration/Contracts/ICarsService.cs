namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICarsService : IBaseService<string>
    {
        Task<IEnumerable<T>> GetAllByParkingAsync<T>(int parkingId);
    }
}
