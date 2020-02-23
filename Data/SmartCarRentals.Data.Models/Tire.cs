namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.Tire;

    public class Tire : BaseDeletableModel<int>
    {
        public Tire()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;

            this.ConditionStatus = ConditionStatus.Ok;
        }

        public DateTime ProductionDate { get; set; }

        public Season Season { get; set; }

        public int KmRun { get; set; }

        public UsingStatus UsingStatus { get; set; }

        public ConditionStatus ConditionStatus { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
