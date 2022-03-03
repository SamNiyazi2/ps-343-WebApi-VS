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
            CreateMap<CourseCreateDTO, Course>();

            // 03/01/2022 05:22 pm - SSN - [20220301-1703] - [005] - M08-03 - Demo: Updating a resource (Part1)
            CreateMap<CourseUpdateDTO, Course>();


            // 03/02/2022 11:35 am - SSN - [20220302-1116] - [002] - M08-11 -Demo:  Partially updating a resource
            CreateMap<Course, CourseUpdateDTO>();

        }

    }
}
