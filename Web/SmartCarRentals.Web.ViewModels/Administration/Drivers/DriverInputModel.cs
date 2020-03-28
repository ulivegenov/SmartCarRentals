namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;

    public class DriverInputModel : IMapTo<DriverServiceInputModel>
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }

        [Required]
        public virtual IFormFile Image { get; set; }
    }
}
