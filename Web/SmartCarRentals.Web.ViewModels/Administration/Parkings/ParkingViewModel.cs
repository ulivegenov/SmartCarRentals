namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;

    public class ParkingViewModel : IMapFrom<ParkingServiceModel>
    {
        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Name { get; set; }

        public int TownId { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.AddressMaxLength)]
        public string Address { get; set; }

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public int Capacity { get; set; }
    }
}
