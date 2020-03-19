namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using System.Collections.Generic;

    public class CountriesAllViewModelCollection
    {
        public CountriesAllViewModelCollection()
        {
            this.Countries = new List<CountriesAllViewModel>();
            this.ParkingByCountry = new Dictionary<string, int>();
        }

        public Dictionary<string, int> ParkingByCountry { get; set; }

        public List<CountriesAllViewModel> Countries { get; set; }
    }
}
