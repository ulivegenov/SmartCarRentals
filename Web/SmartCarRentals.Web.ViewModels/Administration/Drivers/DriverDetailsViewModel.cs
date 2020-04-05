namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models.Enums.Driver;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Drivers;

    public class DriverDetailsViewModel : IMapFrom<DriverServiceDetailsModel>, IMapTo<DriverServiceDetailsModel>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }

        [StringLength(EntitiesAttributeConstraints.UrlMaxLength, MinimumLength = EntitiesAttributeConstraints.UrlMinLength)]
        public string Image { get; set; }

        public double Rating { get; set; }

        public HireStatus HireStatus { get; set; }

        public string TransfersCount { get; set; }
    }
}
