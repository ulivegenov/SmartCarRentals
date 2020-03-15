namespace SmartCarRentals.Services.Models.Countries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class CountriesServiceAllViewModel : IMapFrom<Country>, IMapTo<Country>
    {
        public CountriesServiceAllViewModel()
        {
            this.Towns = new HashSet<Town>();
        }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength =EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
