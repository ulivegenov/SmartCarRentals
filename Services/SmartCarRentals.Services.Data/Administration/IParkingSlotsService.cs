namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;

    public interface IParkingSlotsService
    {
        Task<IEnumerable<ParkingSlot>> GetAllByParkingIdAsync(int parkingId);

        Task<IEnumerable<ParkingSlot>> GetAllByTownIdAsync(int townId);

        Task<IEnumerable<ParkingSlot>> GetAllByCountryIdAsync(int countryId);

        Task<int> DeleteByIdAsync(int slotId);

        Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> slotIds);
    }
}
