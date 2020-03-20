namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class TownsService : AdministrationService<Town, int>, ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownsService(IDeletableEntityRepository<Town> townRepository)
            : base(townRepository)
        {
            this.townRepository = townRepository;
        }

        public override async Task<int> EditAsync(IServiceDetailsModel<int> serviceDetailsModel)
        {
            var townFromDb = await this.townRepository.All()
                                                       .Where(t => t.Id == serviceDetailsModel.Id)
                                                       .Select(t => new Town()
                                                       {
                                                           CountryId = t.CountryId,
                                                       })
                                                       .FirstOrDefaultAsync();
            var town = serviceDetailsModel.To<Town>();

            town.CountryId = townFromDb.CountryId;
            town.Parkings = townFromDb.Parkings;

            this.townRepository.Update(town);
            var result = await this.townRepository.SaveChangesAsync();

            return result;
        }

        public override async Task<T> GetByIdAsync<T>(int id)
        {
            var town = await this.townRepository.All()
                                                .Where(t => id.Equals(t.Id))
                                                .Select(t => new Town
                                                {
                                                    Id = t.Id,
                                                    Name = t.Name,
                                                    Country = t.Country,
                                                    Parkings = t.Parkings.Select(p => new Parking()
                                                    {
                                                        Name = p.Name,
                                                    })
                                                    .ToList(),
                                                })
                                                .FirstOrDefaultAsync();

            var townServiseModel = town.To<T>();

            return townServiseModel;
        }

        public async Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId)
        {
            var towns = await this.townRepository.All()
                                           .Where(t => t.CountryId == countryId)
                                           .ToListAsync();

            return towns;
        }
    }
}
