namespace SmartCarRentals.Services.Data.Tests.Common
{
    using System.Reflection;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Countries;
    using SmartCarRentals.Services.Models.Main.Cars;

    public class MapperInitializer
    {
        public static void InitializeMapper()
        {
            AutoMapperConfig.RegisterMappings(
                typeof(CountryServiceInputModel).GetTypeInfo().Assembly,
                typeof(CountryServiceDetailsModel).GetTypeInfo().Assembly,
                typeof(Country).GetTypeInfo().Assembly,
                typeof(CarsHotOffersServiceModel).GetTypeInfo().Assembly);
        }
    }
}
