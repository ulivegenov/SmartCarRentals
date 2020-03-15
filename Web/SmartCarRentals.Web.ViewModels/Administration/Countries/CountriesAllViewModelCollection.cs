namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using System.Collections.Generic;

    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;

    public class CountriesAllViewModelCollection : IMapFrom<CountriesServiceAllViewModelCollection>
    {
        public CountriesAllViewModelCollection()
        {
            this.Countries = new HashSet<CountriesAllViewModel>();
        }

        public IEnumerable<CountriesAllViewModel> Countries { get; set; }
    }
}
