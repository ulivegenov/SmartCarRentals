namespace SmartCarRentals.Services.Data.Main
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<MyTransfersServiceAllModel>> GetByUser(string userId)
        {
            var transfers = await this.transferRepository.All()
                                                         .Where(t => t.ClientId == userId)
                                                         .Select(t => new Transfer()
                                                         {
                                                             Id = t.Id,
                                                             TransferDate = t.TransferDate,
                                                             Status = t.Status,
                                                             Type = new TransferType()
                                                             {
                                                                 Name = t.Type.Name,
                                                                 Price = t.Type.Price,
                                                             },
                                                             Driver = new Driver()
                                                             {
                                                                 Id = t.Driver.Id,
                                                                 FirstName = t.Driver.FirstName,
                                                                 LastName = t.Driver.LastName,
                                                             },
                                                         })
                                                         .To<MyTransfersServiceAllModel>()
                                                         .ToListAsync();

            return transfers;
        }
    }
}
