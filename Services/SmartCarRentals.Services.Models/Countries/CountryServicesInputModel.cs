namespace SmartCarRentals.Services.Models.Countries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class CountryServicesInputModel : IMapTo<Country>, IMapFrom<Country>
    {
        public CountryServicesInputModel()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.Towns = new HashSet<Town>();
        }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
