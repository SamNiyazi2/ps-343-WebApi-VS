using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/27/2022 01:00 pm - SSN - [20220227-1251] - [003] - M04-03 - Separating entity model and outer facing model

namespace ps_343_webAPI.Helpers
{
    public static class DateTimeOffsetExt
    {
        public static int GetAge(this DateTimeOffset date)
        {
            DateTimeOffset today = DateTime.Today;

            int age = today.Year - date.Year;

            if (date.Month >= today.Month && date.Day > today.Day)
            {
                age--;
            }

            return age;
        }
    }
}
