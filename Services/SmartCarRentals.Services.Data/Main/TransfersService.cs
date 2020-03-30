namespace SmartCarRentals.Services.Data.Main
{
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Main.Contacts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Transfers;

    public class TransfersService : ITransfersService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;

        public TransfersService(IDeletableEntityRepository<Transfer> transferRepository)
        {
            this.transferRepository = transferRepository;
        }

        public async Task<int> CreateAsync(TransferServiceInputModel transferServiceInputModel)
        {
            var transfer = transferServiceInputModel.To<Transfer>();

            await this.transferRepository.AddAsync(transfer);

            var result = await this.transferRepository.SaveChangesAsync();

            return result;
        }
    }
}
