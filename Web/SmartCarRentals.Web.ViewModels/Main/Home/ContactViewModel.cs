namespace SmartCarRentals.Web.ViewModels.Main.Home
{
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;

    public class ContactViewModel
    {
        [Required]
        [StringLength(EntitiesAttributeConstraints.NameMaxLength, MinimumLength = EntitiesAttributeConstraints.NameMinLength)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.SubjectMaxLength, MinimumLength = EntitiesAttributeConstraints.SubjectMinLength)]
        public string Subject { get; set; }

        [Required]
        [StringLength(EntitiesAttributeConstraints.MessageMaxLength, MinimumLength = EntitiesAttributeConstraints.MessageMinLength)]
        public string Message { get; set; }
    }
}
