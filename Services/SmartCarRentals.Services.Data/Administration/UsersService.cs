namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<int> GetCountAsync()
        {
            var users = await this.userRepository.All()
                                                      .Select(u => new ApplicationUser() { Id = u.Id }).ToListAsync();

            return users.Count;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var users = await this.userRepository.All()
                                                 .Select(u => new ApplicationUser()
                                                 {
                                                     Id = u.Id,
                                                     NormalizedUserName = u.NormalizedUserName,
                                                     NormalizedEmail = u.NormalizedEmail,
                                                     FirstName = u.FirstName,
                                                     LastName = u.LastName,
                                                     Rank = u.Rank,
                                                     Points = u.Points,
                                                 })
                                                 .To<T>()
                                                 .ToListAsync();

            return users;
        }

        public async Task<T> GetByIdAsync<T>(string id)
        {
            var user = await this.userRepository.All()
                                                .Where(u => u.Id == id)
                                                 .Select(u => new ApplicationUser()
                                                 {
                                                     Id = u.Id,
                                                     NormalizedUserName = u.NormalizedUserName,
                                                     NormalizedEmail = u.NormalizedEmail,
                                                     FirstName = u.FirstName,
                                                     LastName = u.LastName,
                                                     Rank = u.Rank,
                                                     Points = u.Points,
                                                 })
                                                 .To<T>()
                                                 .FirstOrDefaultAsync();

            return user;
        }
    }
}
