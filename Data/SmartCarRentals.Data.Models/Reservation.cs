namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Reservation;

    public class Reservation : BaseDeletableModel<int>
    {
        public Reservation()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.Status = Status.Awaiting;
        }

        public DateTime ReservationDate { get; set; }

        public Status Status { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
