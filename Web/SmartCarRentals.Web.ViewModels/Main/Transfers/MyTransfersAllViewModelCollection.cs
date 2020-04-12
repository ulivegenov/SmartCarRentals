namespace SmartCarRentals.Web.ViewModels.Main.Transfers
{
    using System.Collections.Generic;

    public class MyTransfersAllViewModelCollection
    {
        public MyTransfersAllViewModelCollection()
        {
            this.MyTransfers = new List<MyTransfersAllViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<MyTransfersAllViewModel> MyTransfers { get; set; }
    }
}
