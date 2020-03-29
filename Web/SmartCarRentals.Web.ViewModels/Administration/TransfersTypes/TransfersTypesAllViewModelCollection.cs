namespace SmartCarRentals.Web.ViewModels.Administration.TransfersTypes
{
    using System.Collections.Generic;

    public class TransfersTypesAllViewModelCollection
    {
        public TransfersTypesAllViewModelCollection()
        {
            this.TransfersTypes = new List<TransfersTypesAllViewModel>();
        }

        public List<TransfersTypesAllViewModel> TransfersTypes { get; set; }
    }
}
