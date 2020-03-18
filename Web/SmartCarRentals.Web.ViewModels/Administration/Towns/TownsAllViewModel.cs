namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;

    public class TownsAllViewModel : IMapFrom<TownsServiceAllModel>
    {
        public TownsAllViewModel()
        {
            this.Parkings = new HashSet<Parking>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CountryName { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; }
    }
}
