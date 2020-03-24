namespace SmartCarRentals.Web.ViewModels.Main.Cars
{
    using System.Collections.Generic;

    public class CarsHotOffersViewModelCollection
    {
        public CarsHotOffersViewModelCollection()
        {
            this.Cars = new List<CarsHotOffersViewModel>();
        }

        public List<CarsHotOffersViewModel> Cars { get; set; }
    }
}
