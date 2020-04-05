namespace SmartCarRentals.Web.ViewModels.Main.Reservations
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Reservations;

    public class ReservationCreateInputModel : IMapTo<ReservationServiceInputModel>
    {
        public DateTime ReservationDate { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
