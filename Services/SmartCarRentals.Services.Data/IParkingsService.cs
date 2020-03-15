namespace SmartCarRentals.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Parkings;

    public interface IParkingsService
    {
        Task<bool> CreateAsync();

        Task<bool> EditAsync(int id);

        Task<bool> DeleteAsync(int id);

        Task<int> GetCountAsync();

        Task<IEnumerable<ParkingServiceModel>> GetAllAsync();
    }
}
