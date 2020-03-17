namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;

    public class TownsService : ITownsService
    {
        private readonly IDeletableEntityRepository<Town> townRepository;

        public TownsService(IDeletableEntityRepository<Town> townRepository)
        {
            this.townRepository = townRepository;
        }

        public async Task<bool> CreateAsync(TownServiceInputModel townServicesInputViewModel)
        {
            var town = townServicesInputViewModel.To<Town>();

            await this.townRepository.AddAsync(town);
            var result = await this.townRepository.SaveChangesAsync();

            return result > 0;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TownsServiceAllModel>> GetAllAsync()
        {
            var towns = await this.townRepository.All()
                                           .To<TownsServiceAllModel>()
                                           .ToListAsync();

            return towns;
        }

        public Town GetByName(string name)
        {
            var town = this.townRepository.All().FirstOrDefault(c => c.Name == name);

            return town;
        }

        public async Task<int> GetCountAsync()
        {
            var towns = await this.townRepository.All().ToListAsync();
            var count = towns.Count;

            return count;
        }
    }
}
