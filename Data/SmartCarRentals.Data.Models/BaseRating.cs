namespace SmartCarRentals.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;

    public abstract class BaseRating : BaseDeletableModel<int>
    {
        [Range(EntitiesAttributeConstraints.MinRatingVote, EntitiesAttributeConstraints.MaxRatingVote)]
        public double RatingVote { get; set; }

        [MaxLength(EntitiesAttributeConstraints.CommentMaxLength)]
        public string Coment { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
