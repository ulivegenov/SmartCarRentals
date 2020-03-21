namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System.Collections.Generic;

    public class CarsAllViewModelCollection
    {
        public CarsAllViewModelCollection()
        {
            this.Cars = new List<CarsAllViewModel>();
        }

        public List<CarsAllViewModel> Cars { get; set; }
    }
}
