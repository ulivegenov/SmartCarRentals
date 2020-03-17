namespace SmartCarRentals.Web.ViewModels.Administration.Countries
{
    using System.Collections.Generic;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Countries;

    public class CountriesAllViewModel : IMapFrom<CountriesServiceAllModel>
    {
        public CountriesAllViewModel()
        {
            this.Towns = new HashSet<Town>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
