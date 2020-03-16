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

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<ParkingsServiceAllModel>> GetAllAsync();
    }
}
