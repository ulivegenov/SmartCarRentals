namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class CarEditInputModel : CarInputModel, IMapTo<CarServiceDetailsModel>, IHaveCustomMappings
    {
        public CarEditInputModel()
        {
            this.Parkings = new HashSet<ParkingsDropDownViewModel>();
        }

        public string Id { get; set; }

        public override IFormFile Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<CarServiceDetailsModel, CarEditInputModel>()
                .ForMember(
                           destination => destination.Image,
                           source => source.Ignore());
        }
    }
}