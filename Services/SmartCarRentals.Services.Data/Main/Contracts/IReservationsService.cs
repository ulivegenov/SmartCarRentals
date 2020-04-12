namespace SmartCarRentals.Services.Data.Main.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Reservations;

    public interface IReservationsService : IBaseService<int>
    {
        Task<IEnumerable<MyReservationsServiceAllModel>> GetByUserAsync(string userId, int? take = null, int skip = 0);

        Task<IEnumerable<MyReservationsServiceAllModel>> GetAllAwaitingReservationsAsync();
    }
}
