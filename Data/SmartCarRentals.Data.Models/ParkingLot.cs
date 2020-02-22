namespace SmartCarRentals.Data.Models
{
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.ParkoingLot;

    public class ParkingLot : BaseDeletableModel<int>
    {
        public ParkingLot(int parkingId)
        {
            this.Status = Status.Free;
            this.ParkingId = parkingId;
        }

        public int Number { get; set; }

        public Status Status { get; set; }

        public int ParkingId { get; set; }

        public Parking Parking { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
