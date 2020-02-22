namespace SmartCarRentals.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;
    using SmartCarRentals.Data.Common.Models;

    public class Image : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(EntitiesAttributeConstraints.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(EntitiesAttributeConstraints.UrlMaxLength)]
        public string Url { get; set; }

        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
