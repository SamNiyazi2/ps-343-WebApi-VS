using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/27/2022 10:51 pm - SSN - [20220227-2241] - [002] - M04-07 - Demo: Working with parent/child relationships

namespace ps_343_webAPI.Models
{
    public class CourseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Guid AuthorId { get; set; }

    }
}
