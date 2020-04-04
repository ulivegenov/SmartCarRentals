namespace SmartCarRentals.Services.Models.Main.Transfers
{
    using System;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TransferServiceDetailsModel : IMapFrom<Transfer>, IMapTo<Transfer>
    {
        public int Id { get; set; }

        public DateTime TransferDate { get; set; }

        public int Points { get; set; }

        public int TransferTypeId { get; set; }

        public TransferType Type { get; set; }

        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        public double Rating { get; set; }

        //public string Comment { get; set; }
    }
}
