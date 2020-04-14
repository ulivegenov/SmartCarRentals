namespace SmartCarRentals.Services.Models.Main.Trips
{
    using System;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Trip;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class MyTripsServiceAllModel : IServiceDetailsModel<int>, IMapFrom<Trip>, IMapTo<Trip>
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? EndDate { get; set; }

        public int? KmRun { get; set; }

        public Status Status { get; set; }

        public decimal Price { get; set; }

        public int Points { get; set; }

        public bool HasPaid { get; set; }

        public bool HasVote { get; set; }

        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
