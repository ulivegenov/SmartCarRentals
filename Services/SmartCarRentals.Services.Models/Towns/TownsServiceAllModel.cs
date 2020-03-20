namespace SmartCarRentals.Services.Models.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class TownsServiceAllModel : IServiceAllModel, IMapFrom<Town>, IMapTo<Town>
    {
        public TownsServiceAllModel()
        {
            this.Parkings = new HashSet<Parking>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public virtual Country Country { get; set; }

        public virtual ICollection<Parking> Parkings { get; set; }
    }
}
