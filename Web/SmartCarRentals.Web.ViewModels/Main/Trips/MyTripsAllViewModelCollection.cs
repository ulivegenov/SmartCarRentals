namespace SmartCarRentals.Web.ViewModels.Main.Trips
{
    using System.Collections.Generic;

    public class MyTripsAllViewModelCollection
    {
        public MyTripsAllViewModelCollection()
        {
            this.MyTrips = new List<MyTripsAllViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<MyTripsAllViewModel> MyTrips { get; set; }
    }
}
