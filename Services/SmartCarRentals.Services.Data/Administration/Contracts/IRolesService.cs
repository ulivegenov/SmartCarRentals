namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;

    public interface IRolesService
    {
        Task<ApplicationRole> GetRoleByIdAsync(string id);

        IEnumerable<ApplicationRole> GetAllRolesAsync();
    }
}
