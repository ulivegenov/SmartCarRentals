namespace SmartCarRentals.Services.Models.Drivers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class DriversServiceAllModel : IMapFrom<Driver>, IMapTo<Driver>
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }

        public double Rating { get; set; }

        public ICollection<DriverRating> Ratings { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
