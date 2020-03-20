namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;

    public interface ITownsService : IAdministrationService<int>
    {
        Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId);
    }
}
