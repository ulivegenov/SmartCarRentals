namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Threading.Tasks;

    public interface IDriversRatingsService
    {
        Task<int> DeleteAllByDriverIdAsync(string driverId);
    }
}
