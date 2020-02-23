namespace SmartCarRentals.Data.Models
{
    using System;

    using SmartCarRentals.Data.Common.Models;

    public class DriverLicense : BaseDeletableModel<string>
    {
        public DriverLicense()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Number { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpireOn { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        private DateTime GetDate(string date) => DateTime.Parse(date);
    }
}
