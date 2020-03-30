namespace SmartCarRentals.Web.ViewModels.Main.Transfers
{
    using System;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Transfers;

    public class MyTransfersAllViewModel : IMapFrom<MyTransfersServiceAllModel>
    {
        public int Id { get; set; }

        public DateTime TransferDate { get; set; }

        public Status Status { get; set; }

        public int Points { get; set; }

        public virtual TransferType Type { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
