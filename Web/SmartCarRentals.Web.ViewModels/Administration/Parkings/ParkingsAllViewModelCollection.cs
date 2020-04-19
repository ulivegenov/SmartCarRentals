namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System.Collections.Generic;

    public class ParkingsAllViewModelCollection
    {
        public ParkingsAllViewModelCollection()
        {
            this.Parkings = new List<ParkingsAllViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<ParkingsAllViewModel> Parkings { get; set; }
    }
}
