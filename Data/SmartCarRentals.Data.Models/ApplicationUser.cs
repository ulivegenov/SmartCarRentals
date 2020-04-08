namespace SmartCarRentals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;
    using SmartCarRentals.Data.Models.Enums.User;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;

            this.IsDeleted = false;
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.Points = 0;
            this.Rank = RankType.User;
            this.CarRatings = new HashSet<CarRating>();
            this.Trips = new HashSet<Trip>();
            this.Transfers = new HashSet<Transfer>();
            this.DriverRatings = new HashSet<DriverRating>();
            this.Reservations = new HashSet<Reservation>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // Additional info
        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string LastName { get; set; }

        [MaxLength(EntitiesAttributeConstraints.AddressMaxLength)]
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        [Range(EntitiesAttributeConstraints.AgeMin, EntitiesAttributeConstraints.AgeMax)]
        public int Age { get; set; }

        public string Nationality { get; set; }

        public RankType Rank { get; set; }

        public int Points { get; set; }

        public int? ParkingId { get; set; }

        public virtual Parking Parking { get; set; }

        public virtual ICollection<CarRating> CarRatings { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<Transfer> Transfers { get; set; }

        public virtual ICollection<DriverRating> DriverRatings { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
