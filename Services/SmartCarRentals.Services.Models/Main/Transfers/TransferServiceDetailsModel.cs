namespace SmartCarRentals.Services.Models.Main.Transfers
{
    using System;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Mapping;

    public class TransferServiceDetailsModel : IMapFrom<Transfer>, IMapTo<Transfer>
    {
        public int Id { get; set; }

        public DateTime TransferDate { get; set; }

        public Status Status { get; set; }

        public int Points { get; set; }

        public int TransferTypeId { get; set; }

        public TransferType Type { get; set; }

        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        public double Rating { get; set; }

        public bool HasPaid { get; set; }

        public bool HasVote { get; set; }
    }
}
