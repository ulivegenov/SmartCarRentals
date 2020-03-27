﻿namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;

    public class DriversService : AdministrationService<Driver, string>, IDriversService
    {
        private readonly IDeletableEntityRepository<Driver> driverRepository;

        public DriversService(IDeletableEntityRepository<Driver> driverRepository)
            : base(driverRepository)
        {
            this.driverRepository = driverRepository;
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
    }
}
