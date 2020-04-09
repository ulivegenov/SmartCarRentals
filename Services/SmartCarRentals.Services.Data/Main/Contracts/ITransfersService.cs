namespace SmartCarRentals.Services.Data.Main.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Transfers;

    public interface ITransfersService
    {
        Task<int> CreateAsync(TransferServiceInputModel transferServiceInputModel);

        Task<IEnumerable<MyTransfersServiceAllModel>> GetByUserAsync(string userId);

        Task<TransferServiceDetailsModel> GetByIdAsync(int transferId);

        Task<bool> IsDriverAvailableByDate(DateTime date, string driverId);

        Task<int> PayByIdAsync(int transferId, string userId);

        Task<int> VoteAsync(int transferId);
    }
}
