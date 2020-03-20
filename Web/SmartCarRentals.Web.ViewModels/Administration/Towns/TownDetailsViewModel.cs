namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Contracts;

    public class TownDetailsViewModel : IDetailsViewModel<int>, IMapFrom<TownServiceDetailsModel>, IMapTo<TownServiceDetailsModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Country Name")]
        public string CountryName { get; set; }

        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [Display(Name = "Count of Parkings")]
        public string ParkingsCount { get; set; }

        public ICollection<Parking> Parkings { get; set; }
    }
}
