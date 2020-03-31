namespace SmartCarRentals.Services.Data.Main.Contacts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Transfers;

    public interface ITransfersService
    {
        Task<int> CreateAsync(TransferServiceInputModel transferServiceInputModel);

        Task<IEnumerable<MyTransfersServiceAllModel>> GetByUser(string userId);

        Task<TransferServiceDetailsModel> GetByIdAsync(int transferId);
    }
}
