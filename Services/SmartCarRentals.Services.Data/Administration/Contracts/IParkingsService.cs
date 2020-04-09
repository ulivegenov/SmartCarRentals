namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Administration.Parkings;

    public interface IParkingsService : IBaseService<int>
    {
        Task<IEnumerable<ParkingsServiceAllModel>> GetAllByTownIdAsync(int townId);

        Task<IEnumerable<Parking>> GetAllByCountryIdAsync(int countryId);

        Task<Parking> GetByCarIdAsync(string id);
    }
}
