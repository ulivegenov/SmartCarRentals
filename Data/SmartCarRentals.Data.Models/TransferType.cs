namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;

    public class TransferType : BaseDeletableModel<int>
    {
        public TransferType()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
