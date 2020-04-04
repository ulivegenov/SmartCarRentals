namespace SmartCarRentals.Services.Models.Main.Transfers
{
    using System;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Mapping;

    public class MyTransfersServiceAllModel : IMapFrom<Transfer>
    {
        public int Id { get; set; }

        public DateTime TransferDate { get; set; }

        public Status Status { get; set; }

        public int Points { get; set; }

        public virtual TransferType Type { get; set; }

        public virtual Driver Driver { get; set; }

        public bool HasPaid { get; set; }

        public bool HasVote { get; set; }
    }
}
