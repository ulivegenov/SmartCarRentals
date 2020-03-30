namespace SmartCarRentals.Services.Data.Main.Contacts
{
    using System.Threading.Tasks;

    using SmartCarRentals.Services.Models.Main.Transfers;

    public interface ITransfersService
    {
        Task<int> CreateAsync(TransferServiceInputModel transferServiceInputModel);
    }
}
