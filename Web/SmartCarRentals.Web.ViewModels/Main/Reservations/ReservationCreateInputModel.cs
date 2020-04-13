namespace SmartCarRentals.Web.ViewModels.Main.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Reservations;
    using SmartCarRentals.Web.Infrastructure.CustomAttributes;

    public class ReservationCreateInputModel : IMapTo<ReservationServiceInputModel>
    {
        [Required]
        [DateRange]
        [Display(Name = "Reservation Date")]
        public DateTime ReservationDate { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        [Display(Name = "Car")]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
