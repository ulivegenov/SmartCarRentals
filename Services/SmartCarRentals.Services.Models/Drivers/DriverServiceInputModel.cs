namespace SmartCarRentals.Services.Models.Drivers
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class DriverServiceInputModel : IServiceInputModel, IMapTo<Driver>
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }
    }
}
