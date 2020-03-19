namespace SmartCarRentals.Web.ViewModels.Administration.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DriversAllViewModelCollection
    {
        public DriversAllViewModelCollection()
        {
            this.Drivers = new List<DriversAllViewModel>();
        }

        public List<DriversAllViewModel> Drivers { get; set; }
    }
}
