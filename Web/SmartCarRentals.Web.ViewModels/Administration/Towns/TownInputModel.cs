namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Countries;

    public class TownInputModel : IMapTo<TownServiceInputModel>
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string CountryId { get; set; }

        public IEnumerable<CountriesDropDownViewModel> Countries { get; set; }
    }
}
