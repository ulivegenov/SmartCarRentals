namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;

    public class CountriesAllViewModel : IMapFrom<CountriesServiceAllModel>
    {
        public CountriesAllViewModel()
        {
            this.Towns = new HashSet<Town>();
        }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
