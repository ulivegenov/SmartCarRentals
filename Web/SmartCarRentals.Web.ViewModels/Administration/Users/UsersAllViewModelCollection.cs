namespace SmartCarRentals.Web.ViewModels.Administration.Users
{
    using System.Collections.Generic;

    public class UsersAllViewModelCollection
    {
        public UsersAllViewModelCollection()
        {
            this.Users = new List<UsersAllViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public List<UsersAllViewModel> Users { get; set; }
    }
}
