namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Parkings;

    public interface IParkingsService
    {
        Task<bool> CreateAsync(ParkingServiceInputModel parkingServiceInputModel);

        Task<bool> EditAsync(int id);

        Task<int> DeleteByIdAsync(int id);

        Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids);

        Task<int> GetCountAsync();

        Task<IEnumerable<ParkingsServiceAllModel>> GetAllAsync();

        Task<ParkingServiceDetailsModel> GetByIdAsync(int id);

        Task<IEnumerable<Parking>> GetAllByTownIdAsync(int townId);

        Task<IEnumerable<Parking>> GetAllByCountryIdAsync(int countryId);
    }
}
