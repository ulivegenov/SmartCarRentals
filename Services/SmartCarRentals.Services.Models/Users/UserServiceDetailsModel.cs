namespace SmartCarRentals.Services.Models.Users
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Mapping;

    public class UserServiceDetailsModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public UserServiceDetailsModel()
        {
            this.ApplicationRoles = new HashSet<ApplicationRole>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
        }

        public string Id { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RankType Rank { get; set; }

        public int Points { get; set; }

        public ICollection<ApplicationRole> ApplicationRoles { get; set; }

        public ICollection<IdentityUserRole<string>> Roles { get; set; }
    }
}
