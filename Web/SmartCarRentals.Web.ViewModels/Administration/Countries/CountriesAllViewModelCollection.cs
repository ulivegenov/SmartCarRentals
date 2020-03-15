namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using System.Collections.Generic;

    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;

    public class CountriesAllViewModelCollection
    {
        public CountriesAllViewModelCollection()
        {
            this.Countries = new List<CountriesAllViewModel>();
        }

        public List<CountriesAllViewModel> Countries { get; set; }
    }
}
