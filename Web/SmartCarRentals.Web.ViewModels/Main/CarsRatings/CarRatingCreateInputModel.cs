namespace SmartCarRentals.Web.ViewModels.Main.CarsRatings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.CarsRating;

    public class CarRatingCreateInputModel : IMapTo<CarRatingServiceInputModel>
    {
        [Range(EntitiesAttributeConstraints.MinRatingVote, EntitiesAttributeConstraints.MaxRatingVote)]
        public double RatingVote { get; set; }

        [MaxLength(EntitiesAttributeConstraints.CommentMaxLength)]
        public string Coment { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
