namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Contracts;

    public class CarDetailsViewModel : IDetailsViewModel<string>, IMapFrom<CarServiceDetailsModel>, IMapTo<CarServiceDetailsModel>
    {
        public CarDetailsViewModel()
        {
            this.Trips = new HashSet<Trip>();
            this.Reservations = new HashSet<Reservation>();
            this.Ratings = new HashSet<CarRating>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Model { get; set; }

        public int PricePerHour { get; set; }

        public int PricePerDay { get; set; }

        public ClassType Class { get; set; }

        public HireStatus HireStatus { get; set; }

        public ReservationStatus ReservationStatus { get; set; }

        public TransmitionType Transmition { get; }

        public FuelType Fuel { get; set; }

        [Range(EntitiesAttributeConstraints.MinPassengers, EntitiesAttributeConstraints.MaxPassengers)]
        public int PassengersCapacity { get; set; }

        public double? Rating => this.GetRating();

        public int? ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

        public virtual ICollection<CarRating> Ratings { get; set; }

        private double? GetRating()
        {
            var rating = this.Ratings.Sum(r => r.RatingVote) / this.Trips.Count;

            return rating;
        }
    }
}
