namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Countries;

    public class CountriesDropDownViewModel : IMapFrom<CountriesServiceDropDownModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
