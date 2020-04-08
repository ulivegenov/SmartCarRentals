namespace SmartCarRentals.Services.Data.Main
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Data.Main.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Transfers;

    public class TransfersService : ITransfersService
    {
        private readonly IDeletableEntityRepository<Transfer> transferRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public TransfersService(
                                IDeletableEntityRepository<Transfer> transferRepository,
                                UserManager<ApplicationUser> userManager)
        {
            this.transferRepository = transferRepository;
            this.userManager = userManager;
        }

        public async Task<int> CreateAsync(TransferServiceInputModel transferServiceInputModel)
        {
            transferServiceInputModel.Status = this.GetStatus(transferServiceInputModel.TransferDate);
            var transfer = transferServiceInputModel.To<Transfer>();

            await this.transferRepository.AddAsync(transfer);

            var result = await this.transferRepository.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<MyTransfersServiceAllModel>> GetByUserAsync(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var discount = GlobalConstants.UserDiscount;

            if (user.Rank == RankType.GoldUser)
            {
                discount = GlobalConstants.GoldUserDiscount;
            }

            if (user.Rank == RankType.PlatinumUser)
            {
                discount = GlobalConstants.PlatinumUserDiscount;
            }

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
                                                             HasPaid = t.HasPaid,
                                                             HasVote = t.HasVote,
                                                         })
                                                         .To<MyTransfersServiceAllModel>()
                                                         .OrderBy(t => t.HasPaid)
                                                         .ThenBy(t => t.TransferDate)
                                                         .ToListAsync();

            foreach (var transfer in transfers)
            {
                transfer.Type.Price -= transfer.Type.Price * discount / 100;
            }

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
                                                            HasPaid = t.HasPaid,
                                                            HasVote = t.HasVote,
                                                        })
                                                        .To<TransferServiceDetailsModel>()
                                                        .FirstOrDefaultAsync();

            return transfer;
        }

        public async Task<int> PayByIdAsync(int transferId, string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);
            var discount = GlobalConstants.UserDiscount;

            if (user.Rank == RankType.GoldUser)
            {
                discount = GlobalConstants.GoldUserDiscount;
            }

            if (user.Rank == RankType.PlatinumUser)
            {
                discount = GlobalConstants.PlatinumUserDiscount;
            }

            var transfer = await this.transferRepository.All()
                                                        .Where(t => t.Id == transferId)
                                                        .Select(t => new Transfer()
                                                        {
                                                            Id = t.Id,
                                                            TransferDate = t.TransferDate,
                                                            Points = t.Points,
                                                            Status = t.Status,
                                                            ClientId = t.ClientId,
                                                            DriverId = t.DriverId,
                                                            TransferTypeId = t.TransferTypeId,
                                                            Type = new TransferType()
                                                            {
                                                                Id = t.Type.Id,
                                                                Name = t.Type.Name,
                                                                Price = t.Type.Price,
                                                            },
                                                            HasPaid = t.HasPaid,
                                                            HasVote = t.HasVote,
                                                        })
                                                        .FirstOrDefaultAsync();

            transfer.HasPaid = true;
            transfer.Type.Price -= transfer.Type.Price * discount / 100;
            transfer.Points = (int)Math.Round(transfer.Type.Price / 10);
            this.transferRepository.Update(transfer);

            var result = await this.transferRepository.SaveChangesAsync();

            return transfer.Points;
        }

        public async Task<int> VoteAsync(int transferId)
        {
            var transfer = await this.transferRepository.GetByIdWithDeletedAsync(transferId);

            transfer.HasVote = true;
            var result = await this.transferRepository.SaveChangesAsync();

            return result;
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
