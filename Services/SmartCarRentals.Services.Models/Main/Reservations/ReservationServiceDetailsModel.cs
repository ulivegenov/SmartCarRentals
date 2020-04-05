namespace SmartCarRentals.Services.Models.Main.Reservations
{
    using System;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Reservation;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class ReservationServiceDetailsModel : IServiceDetailsModel<int>, IMapFrom<Reservation>, IMapTo<Reservation>
    {
        public int Id { get; set; }

        public DateTime ReservationDate { get; set; }

        public Status Status { get; set; }

        public int ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
