namespace SmartCarRentals.Services.Models.Countries
{
    using System.Collections.Generic;

    public class CountriesServiceAllViewModelCollection
    {
        public CountriesServiceAllViewModelCollection()
        {
            this.Countries = new HashSet<CountriesServiceAllViewModel>();
        }

        public IEnumerable<CountriesServiceAllViewModel> Countries { get; set; }
    }
}
