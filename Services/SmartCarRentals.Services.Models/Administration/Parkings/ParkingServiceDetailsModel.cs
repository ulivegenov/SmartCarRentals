namespace SmartCarRentals.Services.Models.Administration.Parkings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class ParkingServiceDetailsModel : IServiceDetailsModel<int>, IMapFrom<Parking>, IMapTo<Parking>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public string TownName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.AddressMaxLength, MinimumLength = EntitiesAttributeConstraints.AddressMinLength)]
        public string Address { get; set; }

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public int Capacity { get; set; }

        public int FreeParkingSlots => this.Capacity - this.CarsCount;

        public int CarsCount { get; set; }

        public string ReservationsCount { get; set; }
    }
}
