namespace SmartCarRentals.Services.Models.Cars
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class CarServiceInputModel : IServiceInputModel, IMapTo<Car>, IMapFrom<Car>
    {
    }
}
