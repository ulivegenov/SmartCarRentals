namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Roles;
    using SmartCarRentals.Services.Models.Users;
    using SmartCarRentals.Web.ViewModels.Administration.Roles;

    public interface IUsersService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<UsersServiceAllModel>> GetAllAsync();

        Task<UserServiceDetailsModel> GetByIdAsync(string id);

        Task<IEnumerable<RolesAllServiceModel>> GetUserRoles(string userId);

        Task<ApplicationUser> EditUserRoles(string id, List<RolesAllViewModel> roles);
    }
}
