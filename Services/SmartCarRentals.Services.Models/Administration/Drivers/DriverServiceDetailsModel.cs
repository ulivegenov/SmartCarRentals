﻿namespace SmartCarRentals.Services.Models.Administration.Drivers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class DriverServiceDetailsModel : IServiceDetailsModel<string>, IMapFrom<Driver>, IMapTo<Driver>
    {
        public DriverServiceDetailsModel()
        {
            this.Transfers = new HashSet<Transfer>();
        }

        public string Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
