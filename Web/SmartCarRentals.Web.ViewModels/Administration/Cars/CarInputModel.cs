namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Contracts;

    public class CarInputModel : IMapTo<CarServiceInputModel>
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Model { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.PlateNumberMaxLength, MinimumLength = EntitiesAttributeConstraints.PlateNumberMinLength)]
        public string PlateNumber { get; set; }

        public int PricePerHour { get; set; }

        public int PricePerDay { get; set; }

        public FuelType Fuel { get; set; }

        [Range(EntitiesAttributeConstraints.MinPassengers, EntitiesAttributeConstraints.MaxPassengers)]
        public int PassengersCapacity { get; set; }

        public double? Rating { get; set; }

        public int? ParkingId { get; set; }
    }
}
