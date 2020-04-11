namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System.Collections.Generic;

    public class DriversAllViewModelCollection
    {
        public DriversAllViewModelCollection()
        {
            this.Drivers = new List<DriversAllViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<DriversAllViewModel> Drivers { get; set; }
    }
}
