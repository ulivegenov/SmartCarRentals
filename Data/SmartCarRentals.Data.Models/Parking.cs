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
        public Parking()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.ParkingSlots = new HashSet<ParkingSlot>();
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

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public int Capacity { get; set; }

        public int FreeParkingSlots { get; set; }

        public virtual ICollection<ParkingSlot> ParkingSlots { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
