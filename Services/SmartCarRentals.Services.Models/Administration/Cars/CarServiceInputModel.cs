namespace SmartCarRentals.Services.Models.Administration.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;
    using SmartCarRentals.Services.Models.Contracts;

    public class CarServiceInputModel : IServiceInputModel, IMapTo<Car>, IMapFrom<Car>
    {
        public CarServiceInputModel()
        {
            this.Parkings = new HashSet<ParkingsServiceDropDownModel>();
        }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Model { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.PlateNumberMaxLength, MinimumLength = EntitiesAttributeConstraints.PlateNumberMinLength)]
        public string PlateNumber { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.UrlMaxLength, MinimumLength = EntitiesAttributeConstraints.UrlMinLength)]
        public string Image { get; set; }

        [Required]
        [Range(EntitiesAttributeConstraints.MinPrice, EntitiesAttributeConstraints.MaxPrice)]
        public int PricePerHour { get; set; }

        [Required]
        [Range(EntitiesAttributeConstraints.MinPrice, EntitiesAttributeConstraints.MaxPrice)]
        public int PricePerDay { get; set; }

        public ClassType Class { get; set; }

        public TransmitionType Transmition { get; set; }

        public FuelType Fuel { get; set; }

        [Range(EntitiesAttributeConstraints.MinPassengers, EntitiesAttributeConstraints.MaxPassengers)]
        public int PassengersCapacity { get; set; }

        public int ParkingId { get; set; }

        public Parking Parking { get; set; }

        public ICollection<ParkingsServiceDropDownModel> Parkings { get; set; }
    }
}
