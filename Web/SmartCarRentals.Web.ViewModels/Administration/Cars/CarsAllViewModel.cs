namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System.Collections.Generic;
    using System.Linq;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Contracts;

    public class CarsAllViewModel : IMapFrom<CarsServiceAllModel>
    {
        public CarsAllViewModel()
        {
            this.Trips = new HashSet<Trip>();
            this.Ratings = new HashSet<CarRating>();
        }

        public string Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public ClassType Class { get; set; }

        public double? Rating => this.GetRating();

        public int? ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<CarRating> Ratings { get; set; }

        private double? GetRating()
        {
            var rating = this.Ratings.Sum(r => r.RatingVote) / this.Trips.Count;

            return rating;
        }
    }
}
