namespace SmartCarRentals.Services.Data.Main.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Trips;

    public interface ITripsService : IBaseService<int>
    {
        Task<IEnumerable<MyTripsServiceAllModel>> GetByUserAsync(string userId);

        Task<int> PayTrip(MyTripsServiceAllModel tripServiceModel);
    }
}
