namespace SmartCarRentals.Services.Models.Administration.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;
    using SmartCarRentals.Services.Models.Contracts;

    public class CarServiceDetailsModel : IServiceDetailsModel<string>, IMapTo<Car>, IMapFrom<Car>
    {
        public CarServiceDetailsModel()
        {
            this.Trips = new HashSet<Trip>();
            this.Reservations = new HashSet<Reservation>();
            this.Ratings = new HashSet<CarRating>();
            this.Parkings = new HashSet<ParkingsServiceDropDownModel>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Model { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.PlateNumberMaxLength)]
        public string PlateNumber { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.UrlMaxLength, MinimumLength = EntitiesAttributeConstraints.UrlMinLength)]
        public string Image { get; set; }

        public int KmRun { get; set; }

        public int PricePerHour { get; set; }

        public int PricePerDay { get; set; }

        public ClassType Class { get; set; }

        public HireStatus HireStatus { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public TransmitionType Transmition { get; set; }

        public FuelType Fuel { get; set; }

        [Range(EntitiesAttributeConstraints.MinPassengers, EntitiesAttributeConstraints.MaxPassengers)]
        public int PassengersCapacity { get; set; }

        public double Rating { get; set; }

        public Parking Parking { get; set; }

        public int? ParkingId { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<CarRating> Ratings { get; set; }

        public virtual ICollection<ParkingsServiceDropDownModel> Parkings { get; set; }
    }
}
