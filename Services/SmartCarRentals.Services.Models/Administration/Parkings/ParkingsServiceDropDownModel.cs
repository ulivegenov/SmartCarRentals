﻿namespace SmartCarRentals.Services.Models.Administration.Parkings
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class ParkingsServiceDropDownModel : IMapFrom<Parking>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }
    }
}
