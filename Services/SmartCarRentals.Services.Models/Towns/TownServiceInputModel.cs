namespace SmartCarRentals.Services.Models.Towns
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class TownServiceInputModel : IServiceInputModel, IMapTo<Town>, IMapFrom<Town>
    {
        public TownServiceInputModel()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.Parkings = new HashSet<Parking>();
        }

        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public string CountryId { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; }
    }
}
