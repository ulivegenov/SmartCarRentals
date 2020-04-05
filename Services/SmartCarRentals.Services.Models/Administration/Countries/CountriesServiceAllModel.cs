namespace SmartCarRentals.Services.Models.Administration.Countries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class CountriesServiceAllModel : IServiceAllModel, IMapFrom<Country>, IMapTo<Country>
    {
        public CountriesServiceAllModel()
        {
            this.Towns = new HashSet<Town>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength =EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
