namespace SmartCarRentals.Services.Data.Main.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Reservations;

    public interface IReservationsService : IBaseService<int>
    {
        Task<IEnumerable<MyReservationsServiceAllModel>> GetByUserAsync(string userId);

        Task<IEnumerable<MyReservationsServiceAllModel>> GetAllAwaitingReservationsAsync();
    }
}
