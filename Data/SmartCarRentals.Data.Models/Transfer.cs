﻿namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

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

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

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

        public DriverRating DriverRating { get; set; }
    }
}
