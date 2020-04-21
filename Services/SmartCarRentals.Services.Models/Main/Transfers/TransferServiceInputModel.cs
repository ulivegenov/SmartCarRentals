namespace SmartCarRentals.Services.Models.Main.Transfers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class TransferServiceInputModel : IServiceInputModel, IMapTo<Transfer>
    {
        [Required]
        public DateTime TransferDate { get; set; }

        public int Points { get; set; }

        [Required]
        public int TransferTypeId { get; set; }

        public virtual TransferType Type { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }

        public int? DriverRatingId { get; set; }
    }
}
