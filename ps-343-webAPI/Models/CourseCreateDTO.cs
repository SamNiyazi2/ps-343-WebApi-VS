using ps_343_webAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// 02/28/2022 12:53 pm - SSN - [20220228-1241] - [002] - M06-04 - Demo: Creating a child resource

namespace ps_343_webAPI.Models
{
    // 03/01/2022 02:07 pm - SSN - [20220301-1405] - [001] - M07-05 - Demo: Class-level input validation with IValidatableObject
    // IValidatableObject
    // 03/01/2022 02:46 pm - SSN - [20220301-1439] - [002] - M07-06 - Demo: Class-level input validation with a custom attribute
    // Replace IValidatableObject with CourseTitleMustBeDifferentFromDescription
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "Description must be different from title. (20220301-1504)")]
    public class CourseCreateDTO //: IValidatableObject
    {

        // 03/01/2022 01:59 pm - SSN - [20220301-1246] - [001] - M07-03 - Demo: Validating input with data annotations

        [Required(ErrorMessage = "Title is required (20220301-1502)")]
        [MaxLength(100, ErrorMessage = "Title should not be longer than {1} characters.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "Description should not be longer than {1} characters.")]
        public string Description { get; set; }


        // 03/01/2022 02:08 pm - SSN - [20220301-1405] - [002] - M07-05 - Demo: Class-level input validation with IValidatableObject
        // 03/01/2022 02:52 pm - SSN - [20220301-1439] - [003] - M07-06 - Demo: Class-level input validation with a custom attribute
        // Replace IValidatableObject with CourseTitleMustBeDifferentFromDescription
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Title.ToLower().Trim() == Description.ToLower().Trim())
        //    {
        //        yield return new ValidationResult("The provided description should be different from the title.", new[] { nameof(CourseCreateDTO) });
        //    }
        //}

    }
}
