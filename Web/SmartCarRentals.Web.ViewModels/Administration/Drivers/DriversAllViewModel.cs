namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System.Collections.Generic;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Drivers;

    public class DriversAllViewModel : IMapFrom<DriversServiceAllModel>
    {
        public DriversAllViewModel()
        {
            this.Ratings = new HashSet<DriverRating>();
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Rating { get; set; }

        public ICollection<DriverRating> Ratings { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
