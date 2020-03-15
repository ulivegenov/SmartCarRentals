namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;

    public class ParkingInputModel : IMapFrom<ParkingServiceInputModel>
    {
        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Name { get; set; }

        public string Town { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.AddressMaxLength)]
        public string Address { get; set; }

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public int Capacity { get; set; }
    }
}
