namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;

    public class Transfer : BaseDeletableModel<int>
    {
        public Transfer()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        public DateTime TransferDate { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.AddressMaxLength)]
        public string StartingLocation { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.AddressMaxLength)]
        public string EndingLokation { get; set; }

        public int KmRun { get; set; }

        public decimal Price { get; set; }

        public int Points { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        [Required]
        public string DriverId { get; set; }

        public virtual Driver Driver { get; set; }
    }
}
