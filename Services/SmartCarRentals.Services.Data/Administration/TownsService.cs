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
        private const string InvalidTownIdErrorMessage = "Town with ID: {0} does not exist.";
        private const string InvalidTownsIdsErrorMessage = "There is no Town with any of these IDs.";

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

        public async Task<int> DeleteByIdAsync(int id)
        {
            var town = await this.townRepository.GetByIdWithDeletedAsync(id);

            if (town == null)
            {
                throw new ArgumentNullException(string.Format(InvalidTownIdErrorMessage, id));
            }

            this.townRepository.Delete(town);
            await this.townRepository.SaveChangesAsync();

            return town.Id;
        }

        public async Task<IEnumerable<int>> DeleteAllByIdAsync(IEnumerable<int> ids)
        {
            var towns = await this.townRepository.All()
                                                 .Where(t => ids.Contains(t.Id))
                                                 .ToListAsync();

            if (towns == null)
            {
                throw new ArgumentNullException(InvalidTownsIdsErrorMessage);
            }

            foreach (var town in towns)
            {
                this.townRepository.Delete(town);
            }

            await this.townRepository.SaveChangesAsync();

            var deletedTownsIds = towns.Select(t => t.Id).ToList();

            return deletedTownsIds;
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

        public async Task<IEnumerable<Town>> GetAllByCountryIdAsync(int countryId)
        {
            var towns = await this.townRepository.All()
                                           .Where(t => t.CountryId == countryId)
                                           .ToListAsync();

            return towns;
        }
    }
}
