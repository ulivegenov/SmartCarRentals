namespace SmartCarRentals.Data.Models
{
    using System;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Trip;

    public class Trip : BaseDeletableModel<int>
    {
        public Trip()
        {
            this.CreatedOn = DateTime.UtcNow;

            this.IsDeleted = false;

            this.Status = this.SetStatus();
        }

        public DateTime EndDate { get; set; }

        public int KmStart { get; set; } // TODO Set Logic

        public int? KmEnd { get; set; }

        public int? KmRun => this.KmEnd - this.KmStart;

        public Status Status { get; set; }

        public decimal Price { get; set; } // TODO Logic

        public int Points { get; set; } // TODO Logic - PointsFormula = + 1 point per 10E

        public string CarId { get; set; }

        public virtual Car Car { get; set; }

        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        private Status SetStatus()
            => (Status)Enum.Parse(typeof(Status), "OnGoing");
    }
}
