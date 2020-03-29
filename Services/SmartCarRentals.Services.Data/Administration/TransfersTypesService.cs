namespace SmartCarRentals.Services.Data.Administration
{
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;

    public class TransfersTypesService : AdministrationService<TransferType, int>, ITransfersTypesService
    {
        private readonly IDeletableEntityRepository<TransferType> transferTypeRepository;

        public TransfersTypesService(IDeletableEntityRepository<TransferType> transferTypeRepository)
            : base(transferTypeRepository)
        {
            this.transferTypeRepository = transferTypeRepository;
        }
    }
}
