namespace SmartCarRentals.Services.Models.Cars
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class CarsServiceAllModel : IServiceAllModel, IMapFrom<Car>
    {
        private double? rating;

        public CarsServiceAllModel()
        {
            this.Trips = new HashSet<Trip>();
            this.Ratings = new HashSet<CarRating>();
        }

        public string Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImgUrl { get; set; }

        public ClassType Class { get; set; }

        public double? Rating
        {
            get { return this.rating; }
            set { this.rating = this.GetRating(); }
        }

        public int? ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<CarRating> Ratings { get; set; }

        private double? GetRating()
        {
            double? rating = 0;

            if (this.Ratings.Count() == 0)
            {
                return rating;
            }

            rating = this.Ratings.Sum(r => r.RatingVote) / this.Trips.Count;

            return rating;
        }
    }
}
