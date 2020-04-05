namespace SmartCarRentals.Web.ViewModels.Main.TransfersTypes
{
    using System.Collections.Generic;

    public class TransfersTypesCollection
    {
        public TransfersTypesCollection()
        {
            this.TransfersTypes = new List<TransfersTypesDropDownViewModel>();
        }

        public List<TransfersTypesDropDownViewModel> TransfersTypes { get; set; }
    }
}
