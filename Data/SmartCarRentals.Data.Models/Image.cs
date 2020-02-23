namespace SmartCarRentals.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        public Image()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.UrlMaxLength)]
        public string Url { get; set; }

        [Required]
        public string CarId { get; set; }

        public virtual Car Car { get; set; }
    }
}
