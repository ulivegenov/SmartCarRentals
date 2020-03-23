namespace SmartCarRentals.Services.Models.Users
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Mapping;

    public class UserServiceDetailsModel : IMapFrom<ApplicationUser>, IMapTo<ApplicationUser>
    {
        public string Id { get; set; }

        public string NormalizedUserName { get; set; }

        public string NormalizedEmail { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public RankType Rank { get; set; }

        public int Points { get; set; }
    }
}
