namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Administration.Towns;

    public interface ITownsService : IBaseService<int>
    {
        Task<IEnumerable<TownsServiceAllModel>> GetAllByCountryIdAsync(int countryId);
    }
}
