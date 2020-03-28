namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System;

    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;

    public class DriverEditInputModel : DriverInputModel, IMapTo<DriverServiceDetailsModel>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public override IFormFile Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<DriverServiceDetailsModel, DriverEditInputModel>()
                .ForMember(
                           destination => destination.Image,
                           source => source.Ignore());
        }
    }
}
