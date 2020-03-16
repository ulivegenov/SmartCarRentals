namespace SmartCarRentals.Services.Models.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TownsServiceAllModel : IMapFrom<Town>, IMapTo<Town>
    {
        public TownsServiceAllModel()
        {
            this.Parkings = new HashSet<Parking>();
        }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; }
    }
}
