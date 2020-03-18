namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;

    public class TownDetailsViewModel : IMapFrom<TownServiceDetailsModel>, IMapTo<TownServiceDetailsModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public IEnumerable<string> ParkingNames { get; set; }

        [Display(Name = "Count of Parkings")]
        public string ParkingsCount { get; set; }
    }
}
