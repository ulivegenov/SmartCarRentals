namespace SmartCarRentals.Web.ViewModels.Administration.Parkings
{
    using System.Collections.Generic;

    public class ParkingsAllViewModelCollection
    {
        public ParkingsAllViewModelCollection()
        {
            this.Parkings = new List<ParkingsAllViewModel>();
        }

        public List<ParkingsAllViewModel> Parkings { get; set; }
    }
}
