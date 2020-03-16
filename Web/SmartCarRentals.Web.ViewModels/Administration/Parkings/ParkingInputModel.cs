namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Parkings;

    public class ParkingInputModel : IMapFrom<ParkingServiceInputModel>, IHaveCustomMappings
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public string Town { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.AddressMaxLength, MinimumLength = EntitiesAttributeConstraints.AddressMinLength)]
        public string Address { get; set; }

        [Range(EntitiesAttributeConstraints.MinCapacity, EntitiesAttributeConstraints.MaxCapacity)]
        public int Capacity { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ParkingInputModel, ParkingServiceInputModel>()
                .ForPath(
                    destination => destination.Town.Name,
                    source => source.MapFrom(origin => origin.Town));
        }
    }
}
