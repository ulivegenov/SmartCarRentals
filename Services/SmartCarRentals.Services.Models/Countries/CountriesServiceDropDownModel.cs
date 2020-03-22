﻿namespace SmartCarRentals.Services.Models.Countries
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class CountriesServiceDropDownModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
