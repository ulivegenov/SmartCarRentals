﻿namespace SmartCarRentals.Services.Models.Parkings
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class ParkingsServiceDropDawnModel : IMapFrom<Parking>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
