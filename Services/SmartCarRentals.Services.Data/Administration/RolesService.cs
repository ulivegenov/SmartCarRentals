namespace SmartCarRentals.Services.Data.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Data.Administration.Contracts;

    public class RolesService : IRolesService
    {
        private readonly IServiceProvider serviceProvider;

        public RolesService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(string id)
        {
            var roleManager = this.serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var role = await roleManager.FindByIdAsync(id);

            return role;
        }

        public IEnumerable<ApplicationRole> GetAllRolesAsync()
        {
            var roleManager = this.serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            var roles = roleManager.Roles.ToList();

            return roles;
        }
    }
}
