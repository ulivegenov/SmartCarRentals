namespace SmartCarRentals.Services.Data.AppServices.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IHangfireService
    {
        Task<int> CancelExpiredReservationsAsync();

        Task<int> SettingUPTransfersStatusByDate();
    }
}
