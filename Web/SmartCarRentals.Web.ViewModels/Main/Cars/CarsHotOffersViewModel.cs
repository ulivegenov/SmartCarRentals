namespace SmartCarRentals.Web.ViewModels.Main.Cars
{
    using SmartCarRentals.Data.Models.Enums.Car;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Cars;

    public class CarsHotOffersViewModel : IMapFrom<CarsHotOffersServiceModel>
    {
        public string Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int PricePerDay { get; set; }

        public string ImgUrl { get; set; }

        public ClassType Class { get; set; }
    }
}
