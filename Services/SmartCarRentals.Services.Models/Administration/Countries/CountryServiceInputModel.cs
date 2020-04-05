namespace SmartCarRentals.Services.Models.Administration.Countries
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class CountryServiceInputModel : IServiceInputModel, IMapTo<Country>, IMapFrom<Country>
    {
        public CountryServiceInputModel()
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
