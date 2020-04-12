namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System;
    using System.Threading.Tasks;

    public interface IDriversService : IBaseService<string>
    {
        Task<bool> IsDriverAvailableByDate(DateTime date, string driverId);
    }
}
