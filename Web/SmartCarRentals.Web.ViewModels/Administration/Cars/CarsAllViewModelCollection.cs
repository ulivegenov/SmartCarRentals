namespace SmartCarRentals.Web.ViewModels.Administration.Cars
{
    using System.Collections.Generic;

    public class CarsAllViewModelCollection
    {
        public CarsAllViewModelCollection()
        {
            this.Cars = new List<CarsAllViewModel>();

            this.Countries = new List<string>();
            this.Towns = new List<string>();
            this.Parkings = new List<string>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<CarsAllViewModel> Cars { get; set; }

        public List<string> Countries { get; set; }

        public List<string> Towns { get; set; }

        public List<string> Parkings { get; set; }
    }
}
