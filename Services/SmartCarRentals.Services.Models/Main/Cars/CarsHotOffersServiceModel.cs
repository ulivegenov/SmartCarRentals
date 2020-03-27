namespace SmartCarRentals.Services.Models.Main.Cars
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;

    public class CarsHotOffersServiceModel : IMapFrom<Car>
    {
        public string Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int PricePerDay { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        public ClassType Class { get; set; }
    }
}
