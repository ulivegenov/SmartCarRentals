namespace SmartCarRentals.Services.Data.Administration
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
    using SmartCarRentals.Services.Mapping;

    public class DriversService : BaseService<Driver, string>, IDriversService
    {
        private readonly IDeletableEntityRepository<Driver> driverRepository;
        private readonly IDeletableEntityRepository<Transfer> transfersRepository;

        public DriversService(
                              IDeletableEntityRepository<Driver> driversRepository,
                              IDeletableEntityRepository<Transfer> transfersRepository)
            : base(driversRepository)
        {
            this.driverRepository = driversRepository;
            this.transfersRepository = transfersRepository;
        }

        public override async Task<T> GetByIdAsync<T>(string id)
        {
            var driver = await this.driverRepository.All()
                                                    .Where(d => d.Id == id)
                                                    .Select(d => new Driver()
                                                    {
                                                        Id = d.Id,
                                                        FirstName = d.FirstName,
                                                        LastName = d.LastName,
                                                        Image = d.Image,
                                                        Rating = d.Ratings.Select(r => r.RatingVote).Average(),
                                                        Transfers = d.Transfers.Select(t => new Transfer()
                                                        {
                                                            Id = t.Id,
                                                        })
                                                        .ToList(),
                                                    })
                                                    .To<T>()
                                                    .FirstOrDefaultAsync();

            return driver;
        }

        public override async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var drivers = await this.driverRepository.All()
                                                    .Select(d => new Driver()
                                                    {
                                                        Id = d.Id,
                                                        FirstName = d.FirstName,
                                                        LastName = d.LastName,
                                                        Image = d.Image,
                                                        Rating = d.Ratings.Select(r => r.RatingVote).Average(),
                                                        Transfers = d.Transfers.Select(t => new Transfer()
                                                        {
                                                            Id = t.Id,
                                                        })
                                                        .ToList(),
                                                    })
                                                    .To<T>()
                                                    .ToListAsync();

            return drivers;
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
    }
}
