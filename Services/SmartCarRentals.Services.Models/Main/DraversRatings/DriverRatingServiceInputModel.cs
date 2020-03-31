namespace SmartCarRentals.Services.Models.Main.DraversRatings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class DriverRatingServiceInputModel : IMapTo<DriverRating>
    {
        [Range(EntitiesAttributeConstraints.MinRatingVote, EntitiesAttributeConstraints.MaxRatingVote)]
        public double RatingVote { get; set; }

        [MaxLength(EntitiesAttributeConstraints.CommentMaxLength)]
        public string Coment { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public int TransferId { get; set; }

        public virtual Transfer Transfer { get; set; }

        [Required]
        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
