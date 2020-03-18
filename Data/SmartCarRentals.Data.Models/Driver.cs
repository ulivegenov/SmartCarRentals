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
            this.CreatedOn = DateTime.Now;
            this.IsDeleted = false;

            this.Ratings = new HashSet<DriverRating>();
            this.Transfers = new HashSet<Transfer>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double Rating { get; set; }

        public ICollection<DriverRating> Ratings { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
