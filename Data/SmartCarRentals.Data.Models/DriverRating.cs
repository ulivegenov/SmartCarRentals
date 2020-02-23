namespace SmartCarRentals.Data.Models
{
    public class DriverRating : BaseRating
    {
        public int TransferId { get; set; }

        public virtual Transfer Transfer { get; set; }

        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
