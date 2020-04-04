namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Transfer;

    public class Transfer : BaseDeletableModel<int>
    {
        public Transfer()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.HasPaid = false;
            this.HasVote = false;
        }

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

        public bool HasPaid { get; set; }

        public bool HasVote { get; set; }
    }
}
