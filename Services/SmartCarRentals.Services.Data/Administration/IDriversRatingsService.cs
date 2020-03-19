namespace SmartCarRentals.Services.Data.Administration
{
    using System.Threading.Tasks;

    public interface IDriversRatingsService
    {
        Task<int> DeleteAllByDriverIdAsync(string driverId);
    }
}
