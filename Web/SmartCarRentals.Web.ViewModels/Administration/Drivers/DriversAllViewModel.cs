namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System.Collections.Generic;

    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Data.Models.Enums.Driver;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Administration.Drivers;

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

        public string Image { get; set; }

        public double Rating { get; set; }

        public HireStatus HireStatus { get; set; }

        public ICollection<DriverRating> Ratings { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
