namespace SmartCarRentals.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using SmartCarRentals.Data.Common.Repositories;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Roles;
    using SmartCarRentals.Services.Models.Users;
    using SmartCarRentals.Web.ViewModels.Administration.Roles;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersService(
                            IDeletableEntityRepository<ApplicationUser> userRepository,
                            RoleManager<ApplicationRole> roleManager,
                            UserManager<ApplicationUser> userManager)
        {
            this.userRepository = userRepository;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public async Task<int> GetCountAsync()
        {
            var users = await this.userRepository.All()
                                                      .Select(u => new ApplicationUser() { Id = u.Id }).ToListAsync();

            return users.Count;
        }

        public async Task<IEnumerable<UsersServiceAllModel>> GetAllAsync()
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
                                                     Roles = u.Roles.Select(r => new IdentityUserRole<string>() { RoleId = r.RoleId }).ToList(),
                                                 })
                                                 .To<UsersServiceAllModel>()
                                                 .ToListAsync();

            foreach (var user in users)
            {
                foreach (var identityRole in user.Roles)
                {
                    var applicationRole = await this.roleManager.FindByIdAsync(identityRole.RoleId);
                    user.ApplicationRoles.Add(applicationRole);
                }
            }

            return users;
        }

        public async Task<UserServiceDetailsModel> GetByIdAsync(string id)
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
                                                     Roles = u.Roles.Select(r => new IdentityUserRole<string>() { RoleId = r.RoleId }).ToList(),
                                                 })
                                                 .To<UserServiceDetailsModel>()
                                                 .FirstOrDefaultAsync();

            foreach (var identityRole in user.Roles)
            {
                var applicationRole = await this.roleManager.FindByIdAsync(identityRole.RoleId);
                user.ApplicationRoles.Add(applicationRole);
            }

            return user;
        }

        public async Task<IEnumerable<RolesAllServiceModel>> GetUserRoles(string userId)
        {
            var user = await this.userRepository.All()
                                                .Where(u => u.Id == userId)
                                                .Select(u => new ApplicationUser()
                                                {
                                                    Id = u.Id,
                                                    UserName = u.UserName,
                                                })
                                                .FirstOrDefaultAsync();

            var applicationRoles = this.roleManager.Roles.ToList();
            var rolesService = applicationRoles.Select(r => r.To<RolesAllServiceModel>()).ToList();

            foreach (var role in rolesService)
            {
                role.UserId = userId;

                if (await this.userManager.IsInRoleAsync(user, role.Name))
                {
                    role.IsSelected = true;
                }
                else
                {
                    role.IsSelected = false;
                }
            }

            return rolesService;
        }

        public async Task<ApplicationUser> EditUserRoles(string id, List<RolesAllViewModel> roles)
        {
            var user = await this.userManager.FindByIdAsync(id);

            if (user == null)
            {
                // TODO
            }

            for (int i = 0; i < roles.Count; i++)
            {
                //var applicationRole = await this.roleManager.FindByIdAsync(roles[i].Id);

                IdentityResult result;


                if (roles[i].IsSelected && !(await this.userManager.IsInRoleAsync(user, roles[i].Name)))
                {
                    result = await this.userManager.AddToRoleAsync(user, roles[i].Name);
                }
                else if (!roles[i].IsSelected && await this.userManager.IsInRoleAsync(user, roles[i].Name))
                {
                    result = await this.userManager.RemoveFromRoleAsync(user, roles[i].Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < roles.Count - 1)
                    {
                        continue;
                    }
                }
            }

            return user;
        }
    }
}
