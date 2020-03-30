namespace SmartCarRentals.Services.Models.Main.Drivers
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class DriversServiceDropDownModel : IMapFrom<Driver>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
