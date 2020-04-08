namespace SmartCarRentals.Web.ViewModels.Main.Trips
{
    using System.Collections.Generic;

    public class MyTripsAllViewModelCollection
    {
        public MyTripsAllViewModelCollection()
        {
            this.MyTrips = new List<MyTripsAllViewModel>();
        }

        public List<MyTripsAllViewModel> MyTrips { get; set; }
    }
}
