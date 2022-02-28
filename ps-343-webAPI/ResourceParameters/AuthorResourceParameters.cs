using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/28/2022 10:01 am - SSN - [20220228-0958] - [001] - M05-07 - Demo: Group action parameters together into one object

namespace ps_343_webAPI.ResourceParameters
{
    public class AuthorResourceParameters
    {
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }
    }
}
