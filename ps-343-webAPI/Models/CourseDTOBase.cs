using ps_343_webAPI.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// 03/01/2022 06:00 pm - SSN - [20220301-1749] - [002] - M08-06 - Validating input when updating a resource with PUT

namespace ps_343_webAPI.Models
{
    [CourseTitleMustBeDifferentFromDescription(ErrorMessage = "Description must be different from title. (20220301-1504)")]
    public abstract class CourseDTOBase
    {


        [Required(ErrorMessage = "Title is required (20220301-1502)")]
        [MaxLength(100, ErrorMessage = "Title should not be longer than {1} characters.")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "Description should not be longer than {1} characters.")]

        ////////////////////////////////////////// public string Description { get; set; }
        public virtual string Description { get; set; }


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

