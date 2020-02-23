﻿namespace SmartCarRentals.Data.Models
{
    using System;

    using SmartCarRentals.Data.Common.Models;

    public class Reservation : BaseDeletableModel<int>
    {
        public Reservation()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime ReservationDate { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
