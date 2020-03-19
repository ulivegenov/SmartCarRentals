namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using SmartCarRentals.Common;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;
    using SmartCarRentals.Web.ViewModels.Administration.Countries;

    public class TownInputModel : IMapTo<TownServiceInputModel>//, IHaveCustomMappings
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Required]
        public string CountryId { get; set; }

        public IEnumerable<CountriesDropDownViewModel> Countries { get; set; }

        //public void CreateMappings(IProfileExpression configuration)
        //{
        //    configuration.CreateMap<TownInputModel, TownServiceInputModel>()
        //        .ForPath(
        //            destination => destination.Country.Name,
        //            source => source.MapFrom(origin => origin.Country));
        //}
    }
}
