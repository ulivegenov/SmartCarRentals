namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Trip;

    public class Trip : BaseDeletableModel<int>
    {
        public Trip()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.Status = Status.Forthcoming;
        }

        public DateTime EndDate { get; set; }

        public int? KmRun { get; set; }

        public Status Status { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int Points { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        public virtual Car Car { get; set; }

        public int? CarRatingId { get; set; }

        public CarRating CarRating { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
