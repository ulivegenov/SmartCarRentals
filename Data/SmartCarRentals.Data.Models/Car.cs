namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Car;

    public class Car : BaseDeletableModel<string>
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.ServiceStatus = ServiceStatus.Ok;
            this.HireStatus = HireStatus.Available;
            this.ReservationStatus = ReservationStatus.Free;
            this.Condition = ConditionType.Normal;

            this.Trips = new HashSet<Trip>();
            this.Ratings = new HashSet<CarRating>();
        }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.PlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        public int KmRun { get; set; }

        [Required]
        [Range(EntitiesAttributeConstraints.MinPrice, EntitiesAttributeConstraints.MaxPrice)]
        public int PricePerHour { get; set; }

        [Required]
        [Range(EntitiesAttributeConstraints.MinPrice, EntitiesAttributeConstraints.MaxPrice)]
        public int PricePerDay { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.UrlMaxLength)]
        public string Image { get; set; }

        public ClassType Class { get; set; }

        public ServiceStatus ServiceStatus { get; set; }

        public HireStatus HireStatus { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public ConditionType Condition { get; set; }

        public TransmitionType Transmition { get; set; }

        public FuelType Fuel { get; set; }

        [Range(EntitiesAttributeConstraints.MinPassengers, EntitiesAttributeConstraints.MaxPassengers)]
        public int PassengersCapacity { get; set; }

        public double? Rating { get; set; }

        public int? ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<CarRating> Ratings { get; set; }
    }
}
