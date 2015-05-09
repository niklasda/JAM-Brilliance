using System;
using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Validators
{
    public class AgeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateValue = (DateTime)value;
            long deltaTicks = DateTime.Now.Ticks - dateValue.Ticks;
            var age = new DateTime(deltaTicks);

            const int minAge = 17;
            const int maxAge = 129;

            string errorMessage = string.Format("Ålder måste vara mellan {0} och {1}. Din ålder är {2} år", minAge, maxAge, age.Year);

            if (!(age.Year >= minAge && age.Year <= maxAge))
            {
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}