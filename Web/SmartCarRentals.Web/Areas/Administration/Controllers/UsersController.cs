namespace SmartCarRentals.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Data.Administration.Contracts;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Web.ViewModels.Administration.Roles;
    using SmartCarRentals.Web.ViewModels.Administration.Users;

    public class UsersController : AdministrationController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await this.usersService.GetByIdAsync(id);
            var viewModel = user.To<UserDetailsViewModel>();

            return this.View(viewModel);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var roles = await this.usersService.GetUserRoles(id);
            var viewModel = new RolesAllViewModelCollection()
            {
                Roles = roles.Select(r => r.To<RolesAllViewModel>()).ToList(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, List<RolesAllViewModel> roles)
        {
            var user = await this.usersService.EditUserRoles(id, roles);

            return this.Redirect("/Administration/Users/All");
        }

        public async Task<IActionResult> All(int id = 1)
        {
            var page = id;
            var users = await this.usersService.GetAllWithPagingAsync(GlobalConstants.UsersPerPageAdmin, (page - 1) * GlobalConstants.UsersPerPageAdmin);
            //var users = await this.usersService.GetAllAsync();

            var viewModel = new UsersAllViewModelCollection()
            {
                Users = users.Select(u => u.To<UsersAllViewModel>()).ToList(),
            };

            var count = await this.usersService.GetCountAsync();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / GlobalConstants.UsersPerPageAdmin);

            if (viewModel.PagesCount == 0)
            {
                viewModel.PagesCount = 1;
            }

            viewModel.CurrentPage = page;

            return this.View(viewModel);
        }
    }
}
