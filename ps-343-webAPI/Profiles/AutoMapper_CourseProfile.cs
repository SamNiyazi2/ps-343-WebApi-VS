using AutoMapper;
using CourseLibrary.API.Entities;
using ps_343_webAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/27/2022 10:54 pm - SSN - [20220227-2241] - [003] - M04-07 - Demo: Working with parent/child relationships

namespace ps_343_webAPI.Profiles
{
    public class AutoMapper_CourseProfile : Profile
    {
        public AutoMapper_CourseProfile()
        {
            CreateMap<Course, CourseDTO>();
        }

    }
}
