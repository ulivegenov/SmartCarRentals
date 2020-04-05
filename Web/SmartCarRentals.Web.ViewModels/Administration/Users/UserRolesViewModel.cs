namespace SmartCarRentals.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Users;

    public class UserRolesViewModel : IMapFrom<UserRolesServiceModel>
    {
        public UserRolesViewModel()
        {
            this.Roles = new HashSet<IdentityUserRole<string>>();
        }

        public string Id { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
