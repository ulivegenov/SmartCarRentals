namespace SmartCarRentals.Services.Models.Main.Transfers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Mapping;

    public class TransferServiceInputModel : IMapTo<Transfer>
    {
        public DateTime TransferDate { get; set; }

        public Status Status { get; set; }

        public int Points { get; set; }

        [Required]
        public int TransferTypeId { get; set; }

        public virtual TransferType Type { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        public int? DriverRatingId { get; set; }
    }
}
