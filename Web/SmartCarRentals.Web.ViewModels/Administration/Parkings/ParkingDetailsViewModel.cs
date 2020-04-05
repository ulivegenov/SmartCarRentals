namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Parkings;

    public class ParkingDetailsViewModel : IMapFrom<ParkingServiceDetailsModel>, IMapTo<ParkingServiceDetailsModel>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Display(Name = "Town Name")]
        public string TownName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.AddressMaxLength, MinimumLength = EntitiesAttributeConstraints.AddressMinLength)]
        public string Address { get; set; }

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public string Capacity { get; set; }

        public string FreeParkingSlots { get; set; }

        public string CarsCount { get; set; }

        public string ReservationsCount { get; set; }
    }
}
