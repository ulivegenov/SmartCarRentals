﻿namespace SmartCarRentals.Services.Models.Drivers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Models;
    using SmartCarRentals.Services.Mapping;
    using SmartCarRentals.Services.Models.Contracts;

    public class DriversServiceAllModel : IServiceAllModel, IMapFrom<Driver>, IMapTo<Driver>
    {
        public string Id { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string LastName { get; set; }

        public double Rating { get; set; }

        public ICollection<DriverRating> Ratings { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }
    }
}
