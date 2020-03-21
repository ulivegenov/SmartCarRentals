namespace SmartCarRentals.Services.Models.Cars
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CarServiceDetailsModel : IServiceDetailsModel<string>, IMapTo<Car>, IMapFrom<Car>
    {
        public string Id { get; set; }
    }
}
