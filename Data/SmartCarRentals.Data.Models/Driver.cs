namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using SmartCarRentals.Data.Common.Models;

    public class Driver : BaseDeletableModel<string>
    {
        public Driver()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Ratings = new HashSet<int>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Rating => this.Ratings.Sum() / this.Ratings.Count;

        public ICollection<int> Ratings { get; set; }
    }
}
