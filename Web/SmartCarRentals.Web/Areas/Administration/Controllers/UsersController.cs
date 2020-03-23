namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Models.Users;
    using SmartCarRentals.Web.ViewModels.Administration.Users;
    using SmartCarRentals.Services.Mapping;
    using System.Linq;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.usersService.GetByIdAsync<UserServiceDetailsModel>(id);
            var viewModel = user.To<UserDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.usersService.GetByIdAsync<UserServiceDetailsModel>(id);
            var viewModel = user.To<UserDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> All()
        {
            var users = await this.usersService.GetAllAsync<UsersServiceAllModel>();
            var viewModel = new UsersAllViewModelCollection();
            viewModel.Users = users.Select(u => u.To<UsersAllViewModel>()).ToList();

            return this.View(viewModel);
        }
    }
}
