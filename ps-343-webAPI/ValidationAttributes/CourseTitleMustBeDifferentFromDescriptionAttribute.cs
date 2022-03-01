using ps_343_webAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// 03/01/2022 02:39 pm - SSN - [20220301-1439] - [001] - M07-06 - Demo: Class-level input validation with a custom attribute

namespace ps_343_webAPI.ValidationAttributes
{
    // Class-level attribute
    public class CourseTitleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var course = (CourseCreateDTO)validationContext.ObjectInstance;
            if ( course.Title.ToLower().Trim() == course.Description.ToLower().Trim())
            {
                string errorMessage = string.IsNullOrWhiteSpace(ErrorMessage) ? "The provided description should be different from the title. (20220301-1444)": ErrorMessage;

                return new ValidationResult(
                        errorMessage,
                        new[] { nameof(CourseCreateDTO) }
                    );
            }

            return ValidationResult.Success;
        }

    }
}
