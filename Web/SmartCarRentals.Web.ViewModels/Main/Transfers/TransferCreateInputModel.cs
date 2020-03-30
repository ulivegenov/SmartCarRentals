namespace SmartCarRentals.Web.ViewModels.Main.Transfers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Transfers;
    using SmartCarRentals.Web.ViewModels.Administration.Drivers;
    using SmartCarRentals.Web.ViewModels.Main.Drivers;
    using SmartCarRentals.Web.ViewModels.Main.TransfersTypes;

    public class TransferCreateInputModel : IMapTo<TransferServiceInputModel>
    {
        public TransferCreateInputModel()
        {
            this.DriverRatingId = null;
            this.TransfersTypes = new HashSet<TransfersTypesDropDownViewModel>();
            this.Drivers = new HashSet<DriverDetailsViewModel>();
        }

        [Required]
        public DateTime TransferDate { get; set; }

        public Status Status { get; set; }

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

        public IEnumerable<TransfersTypesDropDownViewModel> TransfersTypes { get; set; }

        public IEnumerable<DriverDetailsViewModel> Drivers { get; set; }
    }
}
