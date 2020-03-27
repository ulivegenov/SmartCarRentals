﻿namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Cars;
    using SmartCarRentals.Web.ViewModels.Administration.Parkings;

    public class CarInputModel : IMapTo<CarServiceInputModel>, IMapFrom<CarDetailsViewModel>
    {
        public CarInputModel()
        {
            this.Parkings = new HashSet<ParkingsDropDownViewModel>();
        }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Make { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Model { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.PlateNumberMaxLength, MinimumLength = EntitiesAttributeConstraints.PlateNumberMinLength)]
        public string PlateNumber { get; set; }

        [Required]
        [Range(EntitiesAttributeConstraints.MinPrice, EntitiesAttributeConstraints.MaxPrice)]
        public int PricePerHour { get; set; }

        [Required]
        [Range(EntitiesAttributeConstraints.MinPrice, EntitiesAttributeConstraints.MaxPrice)]
        public int PricePerDay { get; set; }

        public IFormFile Image { get; set; }

        public ClassType Class { get; set; }

        public TransmitionType Transmition { get; set; }

        public FuelType Fuel { get; set; }

        [Range(EntitiesAttributeConstraints.MinPassengers, EntitiesAttributeConstraints.MaxPassengers)]
        public int PassengersCapacity { get; set; }

        public int ParkingId { get; set; }

        public ICollection<ParkingsDropDownViewModel> Parkings { get; set; }
    }
}
