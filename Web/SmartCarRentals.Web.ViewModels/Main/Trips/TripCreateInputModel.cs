namespace SmartCarRentals.Web.ViewModels.Main.Trips
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Trips;

    public class TripCreateInputModel : IMapTo<TripServiceInputModel>
    {
        public DateTime? EndDate { get; set; }

        public int? KmRun { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public int Points { get; set; }

        public bool HasPaid { get; set; }

        public bool HasVote { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
