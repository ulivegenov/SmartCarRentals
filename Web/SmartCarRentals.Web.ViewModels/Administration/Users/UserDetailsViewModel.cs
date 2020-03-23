namespace SmartCarRentals.Web.ViewModels.Administration.Users
{
    using SmartCarRentals.Data.Models.Enums.User;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Users;

    public class UserDetailsViewModel : IMapTo<UserServiceDetailsModel>, IMapFrom<UserServiceDetailsModel>
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
