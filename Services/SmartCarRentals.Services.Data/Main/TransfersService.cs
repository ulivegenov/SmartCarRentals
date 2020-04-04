namespace SmartCarRentals.Services.Data.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Data.Main.Contacts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Transfers;

    public class TransfersService : ITransfersService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly IDriversRatingsService driversRatingsService;

        public TransfersService(
                                IDeletableEntityRepository<Transfer> transferRepository,
                                IDriversRatingsService driversRatingsService)
        {
            this.transferRepository = transferRepository;
            this.driversRatingsService = driversRatingsService;
        }

        public async Task<int> CreateAsync(TransferServiceInputModel transferServiceInputModel)
        {
            transferServiceInputModel.Status = this.GetStatus(transferServiceInputModel.TransferDate);
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
                                                                 Id = t.Type.Id,
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

        public async Task<TransferServiceDetailsModel> GetByIdAsync(int transferId)
        {
            var transfer = await this.transferRepository.All()
                                                        .Where(t => t.Id == transferId)
                                                        .Select(t => new Transfer()
                                                        {
                                                            Id = t.Id,
                                                            TransferDate = t.TransferDate,
                                                            Points = t.Points,
                                                            Type = new TransferType()
                                                            {
                                                                Id = t.Type.Id,
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
                                                        .To<TransferServiceDetailsModel>()
                                                        .FirstOrDefaultAsync();

            return transfer;
        }

        // Temp Method, only for Development
        private Status GetStatus(DateTime transferDate)
        {
            var status = Status.Forthcoming;

            if (transferDate.CompareTo(DateTime.UtcNow) == 0)
            {
                status = Status.OnGoing;
            }

            if (transferDate.CompareTo(DateTime.UtcNow) < 0)
            {
                status = Status.Finished;
            }

            return status;
        }
    }
}
