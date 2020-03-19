namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class CountriesDropDownViewModel : IMapFrom<Country>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
