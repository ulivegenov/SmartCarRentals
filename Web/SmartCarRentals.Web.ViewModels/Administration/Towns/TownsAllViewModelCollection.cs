﻿namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.Linq;

    public class TownsAllViewModelCollection
    {
        public TownsAllViewModelCollection()
        {
            this.Towns = new List<TownsAllViewModel>();
        }

        public List<TownsAllViewModel> Towns { get; set; }
    }
}
