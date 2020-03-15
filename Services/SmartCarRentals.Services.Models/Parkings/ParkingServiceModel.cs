namespace SmartCarRentals.Services.Models.Parkings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.ParkoingLot;
    using SmartCarRentals.Services.Mapping;

    public class ParkingServiceModel : IMapFrom<Parking>, IMapTo<Parking>
    {
        public ParkingServiceModel()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.ParkingSlots = new HashSet<ParkingSlot>();
            this.Cars = new HashSet<Car>();
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

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

        public int FreeParkingLots => this.GetFreeParkingLots();

        public virtual ICollection<ParkingSlot> ParkingSlots { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<ApplicationUser> Workers { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        private int GetFreeParkingLots()
            => this.ParkingSlots.Where(pl => pl.Status == Status.Free).Count();
    }
}
