using System;
using System.ComponentModel.DataAnnotations;

namespace JAM.Validators
{
    public class AgeValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateValue = (DateTime)value;
            long deltaTicks = DateTime.Now.Ticks - dateValue.Ticks;
            var age = new DateTime(deltaTicks);

            const int MinAge = 17;
            const int MaxAge = 129;

            string errorMessage = string.Format("Ålder måste vara mellan {0} och {1}. Din ålder är {2} år", MinAge, MaxAge, age.Year);

            if (!(age.Year >= MinAge && age.Year <= MaxAge))
            {
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }
    }
}