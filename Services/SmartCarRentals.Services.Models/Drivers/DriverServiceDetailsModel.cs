namespace SmartCarRentals.Services.Models.Drivers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class DriverServiceDetailsModel : IMapFrom<Driver>, IMapTo<Driver>
    {
        public DriverServiceDetailsModel()
        {
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
