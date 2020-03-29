namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Driver;

    public class Driver : BaseDeletableModel<string>
    {
        public Driver()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;

            this.HireStatus = HireStatus.Available;
            this.Ratings = new HashSet<DriverRating>();
            this.Transfers = new HashSet<Transfer>();
        }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string LastName { get; set; }

        public double Rating { get; set; }

        public string Image { get; set; }

        public HireStatus HireStatus { get; set; }

        public ICollection<DriverRating> Ratings { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
