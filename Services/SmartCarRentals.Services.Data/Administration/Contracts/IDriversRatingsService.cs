namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.DraversRatings;

    public interface IDriversRatingsService
    {
        Task<int> DeleteAllByDriverIdAsync(string driverId);

        Task<DriverRatingServiceDetailsModel> GetByTransferId(int transferId);

        Task<int> CreateAsync(DriverRatingServiceInputModel driverRatingServiceInputModel);
    }
}
