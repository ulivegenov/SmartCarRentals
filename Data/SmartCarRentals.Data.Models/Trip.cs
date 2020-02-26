namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Trip;

    public class Trip : BaseDeletableModel<int>
    {
        public Trip()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.Status = Status.OnGoing;
        }

        public DateTime EndDate { get; set; }

        public int KmStart { get; set; }

        public int? KmEnd { get; set; }

        public int? KmRun => this.KmEnd - this.KmStart;

        public Status Status { get; set; }

        public decimal Price { get; set; }

        public int Points { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        public virtual Car Car { get; set; }

        public int CarRatingId { get; set; }

        public CarRating CarRating { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
