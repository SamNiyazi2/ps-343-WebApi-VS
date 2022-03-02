using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

// 03/01/2022 05:12 pm - SSN - [20220301-1703] - [004] - M08-03 - Demo: Updating a resource (Part1)

namespace ps_343_webAPI.Models
{
    // 03/01/2022 06:04 pm - SSN - [20220301-1749] - [004] - M08-06 - Validating input when updating a resource with PUT
    public class CourseUpdateDTO : CourseDTOBase
    {

        // 03/01/2022 05:53 pm - SSN - [20220301-1749] - [001] - M08-06 - Validating input when updating a resource with PUT

        // 03/01/2022 06:04 pm - SSN - [20220301-1749] - [004] - M08-06 - Validating input when updating a resource with PUT

        //public string Title { get; set; }

        [Required(ErrorMessage = "Description is required (20220301-1756)")]
        override public string Description { get => base.Description; set => base.Description = value; }
    }
}
