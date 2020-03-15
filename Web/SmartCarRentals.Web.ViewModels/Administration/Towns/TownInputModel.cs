namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Microsoft.AspNetCore.Mvc.Rendering;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;
    using SmartCarRentals.Services.Models.Towns;

    public class TownInputModel : IMapTo<TownsServiceInputModel>, IHaveCustomMappings
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        public List<SelectListItem> CountryOptions { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<TownInputModel, TownsServiceInputModel>()
                .ForMember(
                    destination => destination.Country,
                    source => source.MapFrom(origin => new CountryServiceInputModel
                    {
                        Name = origin.Country,
                    }));
        }
    }
}
