namespace SmartCarRentals.Web.ViewModels.Main.Drivers
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Main.Drivers;

    public class DriversDropDownViewModel : IMapFrom<DriversServiceDropDownModel>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
