namespace SmartCarRentals.Services.Data.AppServices.Contracts
{
    using System.Threading.Tasks;

    public interface IHangfireService
    {
        Task<int> CancelExpiredReservations();
    }
}
