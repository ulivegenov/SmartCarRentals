namespace SmartCarRentals.Services.Models.Main.DraversRatings
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class DriverRatingServiceDetailsModel : IMapFrom<DriverRating>
    {
        public double RatingVote { get; set; }

        public string Coment { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        public int TransferId { get; set; }

        public virtual Transfer Transfer { get; set; }

        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
