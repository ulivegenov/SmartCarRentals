﻿namespace SmartCarRentals.Data.Models
{
    using System;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.ParkoingLot;

    public class ParkingSlot : BaseDeletableModel<int>
    {
        public ParkingSlot()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.Status = Status.Free;
            this.CarId = null;
        }

        public int Number { get; set; }

        public Status Status { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
