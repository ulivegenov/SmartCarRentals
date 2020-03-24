namespace SmartCarRentals.Services.Models.Users
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class UserRolesServiceModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public UserRolesServiceModel()
        {
            this.Roles = new HashSet<IdentityUserRole<string>>();
        }

        public string Id { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
