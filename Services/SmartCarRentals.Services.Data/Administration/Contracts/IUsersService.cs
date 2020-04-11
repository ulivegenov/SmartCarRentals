namespace SmartCarRentals.Services.Data.Administration.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Models.Administration.Roles;
    using SmartCarRentals.Services.Models.Administration.Users;
    using SmartCarRentals.Web.ViewModels.Administration.Roles;

    public interface IUsersService
    {
        Task<int> GetCountAsync();

        Task<IEnumerable<UsersServiceAllModel>> GetAllAsync();

        Task<UserServiceDetailsModel> GetByIdAsync(string id);

        Task<IEnumerable<UsersServiceAllModel>> GetAllWithPagingAsync(int? take = null, int skip = 0);

        Task<IEnumerable<RolesAllServiceModel>> GetUserRoles(string userId);

        Task<ApplicationUser> EditUserRoles(string id, List<RolesAllViewModel> roles);

        Task<bool> GetPointsAsync(string id, int points);
    }
}
