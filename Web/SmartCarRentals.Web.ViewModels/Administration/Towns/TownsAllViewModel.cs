namespace SmartCarRentals.Web.ViewModels.Administration.Towns
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Towns;

    public class TownsAllViewModel : IMapFrom<TownsServiceAllModel>
    {
        public TownsAllViewModel()
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
