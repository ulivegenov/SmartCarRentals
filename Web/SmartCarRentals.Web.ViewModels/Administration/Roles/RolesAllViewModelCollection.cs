namespace SmartCarRentals.Web.ViewModels.Administration.Roles
{
    using System.Collections.Generic;

    public class RolesAllViewModelCollection
    {
        public RolesAllViewModelCollection()
        {
            this.Roles = new List<RolesAllViewModel>();
        }

        public List<RolesAllViewModel> Roles { get; set; }
    }
}
