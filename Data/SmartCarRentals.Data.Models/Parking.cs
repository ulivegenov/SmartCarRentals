namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.ParkoingLot;

    public class Parking : BaseDeletableModel<int>
    {
        public Parking(int capacity)
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.ParkingLots = new HashSet<ParkingLot>(capacity);
            this.Cars = new HashSet<Car>();
            this.Reservations = new HashSet<Reservation>();
        }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Name { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.AddressMaxLength)]
        public string Address { get; set; }

        public int FreeParkingLots => this.GetFreeParkingLots();

        public virtual ICollection<ParkingLot> ParkingLots { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<ApplicationUser> Workers { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        private int GetFreeParkingLots()
            => this.ParkingLots.Where(pl => pl.Status == Status.Free).Count();
    }
}
