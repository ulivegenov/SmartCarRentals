namespace SmartCarRentals.Web.ViewModels.Main.Reservations
{
    using System.Collections.Generic;

    public class MyReservationsAllViewModelCollection
    {
        public MyReservationsAllViewModelCollection()
        {
            this.MyReservations = new List<MyReservationsAllViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<MyReservationsAllViewModel> MyReservations { get; set; }
    }
}
