namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;

    public class TownInputModel : IMapTo<TownServiceInputModel>, IHaveCustomMappings
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TownInputModel, TownServiceInputModel>()
                .ForPath(
                    destination => destination.Country.Name,
                    source => source.MapFrom(origin => origin.Country));
        }
    }
}
