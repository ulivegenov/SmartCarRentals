namespace SmartCarRentals.Services.Models.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;

    public class TownServiceDetailsModel : IMapFrom<Town>, IMapTo<Town>
    {
        public TownServiceDetailsModel()
        {
            this.Parkings = new HashSet<Parking>();
            this.ParkingNames = new HashSet<string>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        public Country Country { get; set; }

        public IEnumerable<string> ParkingNames { get; set; }

        public ICollection<Parking> Parkings { get; set; }
    }
}
