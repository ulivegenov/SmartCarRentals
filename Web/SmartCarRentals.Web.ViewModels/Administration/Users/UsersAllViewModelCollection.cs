namespace SmartCarRentals.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    public class UsersAllViewModelCollection
    {
        public UsersAllViewModelCollection()
        {
            this.Users = new List<UsersAllViewModel>();
        }

        public List<UsersAllViewModel> Users { get; set; }
    }
}
