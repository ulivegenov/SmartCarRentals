namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Threading.Tasks;

    public interface IAdministrationService
    {
        Task<int> GetCountAsync();
    }
}
