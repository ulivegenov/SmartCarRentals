namespace SmartCarRentals.Services.Data.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Transfers;

    public class TransfersService : BaseService<Transfer, int>, ITransfersService
    {
        private readonly IDeletableEntityRepository<Transfer> transfersRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public TransfersService(
                                IDeletableEntityRepository<Transfer> transfersRepository,
                                IDeletableEntityRepository<ApplicationUser> usersRepository)
            : base(transfersRepository)
        {
            this.transfersRepository = transfersRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<IEnumerable<MyTransfersServiceAllModel>> GetByUserAsync(string userId, int? take = null, int skip = 0)
        {
            var user = await this.usersRepository.All()
                                                 .Where(u => u.Id == userId)
                                                 .FirstOrDefaultAsync();
            var discount = GlobalConstants.UserDiscount;

            if (user.Rank == RankType.GoldUser)
            {
                discount = GlobalConstants.GoldUserDiscount;
            }

            if (user.Rank == RankType.PlatinumUser)
            {
                discount = GlobalConstants.PlatinumUserDiscount;
            }

            var transfers = this.transfersRepository.All()
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
                                                           Price = t.Type.Price - (t.Type.Price * discount / 100),
                                                       },
                                                       Driver = new Driver()
                                                       {
                                                           Id = t.Driver.Id,
                                                           FirstName = t.Driver.FirstName,
                                                           LastName = t.Driver.LastName,
                                                       },
                                                       HasPaid = t.HasPaid,
                                                       HasVote = t.HasVote,
                                                   })
                                                   .To<MyTransfersServiceAllModel>()
                                                   .OrderByDescending(t => t.TransferDate)
                                                   .ThenBy(t => t.HasPaid)
                                                   .Skip(skip);

            if (take.HasValue)
            {
                transfers = transfers.Take(take.Value);
            }

            return await transfers.ToListAsync();
        }

        public override async Task<TransferServiceDetailsModel> GetByIdAsync<TransferServiceDetailsModel>(int transferId)
        {
            var transfer = await this.transfersRepository.All()
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
                                                             HasPaid = t.HasPaid,
                                                             HasVote = t.HasVote,
                                                         })
                                                         .To<TransferServiceDetailsModel>()
                                                         .FirstOrDefaultAsync();

            return transfer;
        }

        public async Task<bool> IsDriverAvailableByDate(DateTime date, string driverId)
        {
            var transfer = await this.transfersRepository.All()
                                                         .Where(t => t.TransferDate.Date.CompareTo(date.Date) == 0
                                                                && t.Status != Status.Finished)
                                                         .Select(t => new Transfer()
                                                         {
                                                             DriverId = t.DriverId,
                                                         })
                                                         .ToListAsync();

            var result = !transfer.Select(t => t.DriverId).Contains(driverId);

            return result;
        }

        public async Task<int> PayByIdAsync(int transferId, string userId)
        {
            var user = await this.usersRepository.All()
                                                 .Where(u => u.Id == userId)
                                                 .FirstOrDefaultAsync();

            var discount = GlobalConstants.UserDiscount;

            if (user.Rank == RankType.GoldUser)
            {
                discount = GlobalConstants.GoldUserDiscount;
            }

            if (user.Rank == RankType.PlatinumUser)
            {
                discount = GlobalConstants.PlatinumUserDiscount;
            }

            var transferDto = await this.transfersRepository.All()
                                                         .Where(t => t.Id == transferId)
                                                         .Select(t => new TransferServicePointsModel()
                                                         {
                                                             Id = t.Id,
                                                             Points = t.Points,
                                                             Status = t.Status,
                                                             TransferTypeId = t.TransferTypeId,
                                                             Type = new TransferType()
                                                             {
                                                                 Name = t.Type.Name,
                                                                 Price = t.Type.Price,
                                                             },
                                                             HasPaid = t.HasPaid,
                                                         })
                                                         .FirstOrDefaultAsync();

            transferDto.HasPaid = true;
            transferDto.Type.Price -= transferDto.Type.Price * discount / 100;
            transferDto.Points = (int)Math.Round(transferDto.Type.Price / 10);
            transferDto.Status = Status.Finished;

            var transfer = await this.transfersRepository.GetByIdWithDeletedAsync(transferId);
            transfer.HasPaid = transferDto.HasPaid;
            transfer.Points = transferDto.Points;
            transfer.Status = transferDto.Status;
            this.transfersRepository.Update(transfer);

            await this.transfersRepository.SaveChangesAsync();

            return transfer.Points;
        }

        public async Task<int> VoteAsync(int transferId)
        {
            var transfer = await this.transfersRepository.GetByIdWithDeletedAsync(transferId);

            transfer.HasVote = true;
            var result = await this.transfersRepository.SaveChangesAsync();

            return result;
        }
    }
}
