namespace SmartCarRentals.Services.Models.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class TownServiceDetailsModel : IServiceDetailsModel<int>, IMapFrom<Town>, IMapTo<Town>
    {
        public TownServiceDetailsModel()
        {
            this.Parkings = new HashSet<Parking>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public string CountryName { get; set; }

        public virtual Country Country { get; set; }

        public ICollection<Parking> Parkings { get; set; }
    }
}
