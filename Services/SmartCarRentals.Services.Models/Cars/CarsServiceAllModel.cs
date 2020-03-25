namespace SmartCarRentals.Services.Models.Cars
{
    using System.Collections.Generic;
    using System.Linq;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class CarsServiceAllModel : IServiceAllModel, IMapFrom<Car>
    {
        public CarsServiceAllModel()
        {
            this.Trips = new HashSet<Trip>();
            this.Ratings = new HashSet<CarRating>();
        }

        public string Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string ImgUrl { get; set; }

        public int PricePerDay { get; set; }

        public ClassType Class { get; set; }

        public double? Rating { get; set; }

        public int? ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<CarRating> Ratings { get; set; }
    }
}
