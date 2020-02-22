namespace SmartCarRentals.Data.Models
{
    using SmartCarRentals.Data.Common.Models;

    public class CarClientTrip : BaseDeletableModel<int>
    {
        public string ClientId { get; set; }

        public ApplicationUser Client { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }

        public int TripId { get; set; }

        public Trip Trip { get; set; }
    }
}
