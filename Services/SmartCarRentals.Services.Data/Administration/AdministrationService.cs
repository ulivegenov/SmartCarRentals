namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public abstract class AdministrationService<TEntity, TKey> : IAdministrationService<TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        private const string InvalidTEntityIdErrorMessage = "{0} with ID: {1} does not exist.";
        private const string InvalidTEntityIdsErrorMessage = "There is no {0} with any of these IDs.";

        private readonly IDeletableEntityRepository<TEntity> deletableEntityRepository;

        public AdministrationService(IDeletableEntityRepository<TEntity> deletableEntityRepository)
        {
            this.deletableEntityRepository = deletableEntityRepository;
        }

        public async Task<int> GetCountAsync()
        {
            var deletableEntities = await this.deletableEntityRepository.All()
                                                                        .Select(e => e.Id)
                                                                        .ToListAsync();
            var count = deletableEntities.Count;

            return count;
        }

        public virtual async Task<int> CreateAsync(IServiceInputModel servicesInputViewModel)
        {
            var entity = servicesInputViewModel.To<TEntity>();

            await this.deletableEntityRepository.AddAsync(entity);
            var result = await this.deletableEntityRepository.SaveChangesAsync();

            return result;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var entity = await this.deletableEntityRepository.All()
                                                            .To<T>()
                                                            .ToListAsync();

            return entity;
        }

        public virtual async Task<T> GetByIdAsync<T>(TKey id)
        {
            var entity = await this.deletableEntityRepository.All()
                                                           .Where(t => id.Equals(t.Id))
                                                           .FirstOrDefaultAsync();

            var entityServiseModel = entity.To<T>();

            return entityServiseModel;
        }

        public virtual async Task<int> EditAsync(IServiceDetailsModel<TKey> serviceDetailsModel)
        {
            var entity = serviceDetailsModel.To<TEntity>();

            this.deletableEntityRepository.Update(entity);
            var result = await this.deletableEntityRepository.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteByIdAsync(TKey id)
        {
            var entity = await this.deletableEntityRepository.GetByIdWithDeletedAsync(id);

            if (entity == null)
            {
                throw new ArgumentNullException(string.Format(InvalidTEntityIdErrorMessage, nameof(TEntity), id));
            }

            this.deletableEntityRepository.Delete(entity);
            var result = await this.deletableEntityRepository.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<TKey>> DeleteAllByIdAsync(IEnumerable<TKey> ids)
        {
            var towns = await this.deletableEntityRepository.All()
                                                            .Where(t => ids.Contains<TKey>(t.Id))
                                                            .ToListAsync();

            if (towns == null)
            {
                throw new ArgumentNullException(InvalidTEntityIdsErrorMessage, nameof(TEntity));
            }

            foreach (var town in towns)
            {
                this.deletableEntityRepository.Delete(town);
            }

            await this.deletableEntityRepository.SaveChangesAsync();

            var deletedTownsIds = towns.Select(t => t.Id).ToList();

            return deletedTownsIds;
        }
    }
}
