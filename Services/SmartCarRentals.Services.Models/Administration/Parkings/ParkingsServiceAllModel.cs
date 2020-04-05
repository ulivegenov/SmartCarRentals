namespace SmartCarRentals.Services.Models.Administration.Parkings
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class ParkingsServiceAllModel : IServiceAllModel, IMapTo<Parking>, IMapFrom<Parking>
    {
        public ParkingsServiceAllModel()
        {
            this.ParkingSlots = new HashSet<ParkingSlot>();
            this.Cars = new HashSet<Car>();
            this.Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual Town Town { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.AddressMaxLength, MinimumLength = EntitiesAttributeConstraints.AddressMinLength)]
        public string Address { get; set; }

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public int Capacity { get; set; }

        public virtual ICollection<ParkingSlot> ParkingSlots { get; set; }

        public virtual ICollection<Car> Cars { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
