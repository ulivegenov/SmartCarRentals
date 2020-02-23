namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Common.Models;

    public class DriverLicense : BaseDeletableModel<string>
    {
        public DriverLicense()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        public string Number { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpireOn { get; set; }
    }
}
