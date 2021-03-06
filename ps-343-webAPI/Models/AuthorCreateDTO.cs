using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/28/2022 10:53 am - SSN - [20220228-1037] - [002] - M06-03 - Demo: Creating a resource

namespace ps_343_webAPI.Models
{

    public class AuthorCreateDTO
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTimeOffset DateOfBirth { get; set; }

        public string MainCategory { get; set; }


        // 02/28/2022 03:49 pm - SSN - [20220228-1546] - [001] - M06-05 - Creating child resources together with a parent resource

        public ICollection<CourseCreateDTO> Courses { get; set; } = new List<CourseCreateDTO>();


    }
}
