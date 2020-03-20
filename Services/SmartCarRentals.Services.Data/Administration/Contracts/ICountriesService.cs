namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Countries;

    public interface ICountriesService : IAdministrationService<int>
    {
        Task<CountryServiceDetailsModel> GetByIdAsync(int id);
    }
}
