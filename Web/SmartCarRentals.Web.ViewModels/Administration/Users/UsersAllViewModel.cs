namespace SmartCarRentals.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Users;

    public class UsersAllViewModel : IMapFrom<UsersServiceAllModel>, IMapTo<UsersServiceAllModel>
    {
        public UsersAllViewModel()
        {
            this.ApplicationRoles = new HashSet<ApplicationRole>();
            this.IdentityUserRoles = new HashSet<IdentityUserRole<string>>();
        }

        public string Id { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RankType Rank { get; set; }

        public int Points { get; set; }

        public ICollection<ApplicationRole> ApplicationRoles { get; set; }

        public ICollection<IdentityUserRole<string>> IdentityUserRoles { get; set; }
    }
}
