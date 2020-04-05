namespace SmartCarRentals.Web.ViewModels.Main.Reservations
{
    using System.Collections.Generic;

    public class MyReservationsAllViewModelCollection
    {
        public MyReservationsAllViewModelCollection()
        {
            this.MyReservations = new List<MyReservationsAllViewModel>();
        }

        public List<MyReservationsAllViewModel> MyReservations { get; set; }
    }
}
