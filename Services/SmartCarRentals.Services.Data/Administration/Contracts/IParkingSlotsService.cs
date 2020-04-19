namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;

    public interface IParkingSlotsService : IBaseService<int>
    {
        Task<IEnumerable<ParkingSlot>> GetAllByParkingIdAsync(int parkingId);

        Task<IEnumerable<ParkingSlot>> GetAllByTownIdAsync(int townId);

        Task<IEnumerable<ParkingSlot>> GetAllByCountryIdAsync(int countryId);
    }
}
