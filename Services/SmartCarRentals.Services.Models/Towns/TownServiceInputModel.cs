namespace SmartCarRentals.Services.Models.Towns
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TownServiceInputModel : IMapTo<Town>, IMapFrom<Town>
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

        public int CountryId { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; }
    }
}
