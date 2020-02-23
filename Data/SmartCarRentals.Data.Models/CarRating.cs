namespace SmartCarRentals.Data.Models
{
    public class CarRating : BaseRating
    {
        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
