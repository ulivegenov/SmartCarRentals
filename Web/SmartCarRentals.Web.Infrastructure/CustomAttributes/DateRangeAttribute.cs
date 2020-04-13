namespace SmartCarRentals.Web.Infrastructure.CustomAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using SmartCarRentals.Common;

    public class DateRangeAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;

            if (DateTime.UtcNow.CompareTo(value) >= 0)
            {
                return new ValidationResult(EntitiesAttributeConstraints.InvalidDateMessage);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
