using AutoMapper;
using CourseLibrary.API.Entities;
using ps_343_webAPI.Helpers;
using ps_343_webAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/27/2022 07:16 pm - SSN - [20220227-1912] - [002] - M04-05 - Demo: Adding AutoMapper to our project

namespace ps_343_webAPI.Profiles
{
    public class AutoMapper_AuthorProfile : Profile
    {
        public AutoMapper_AuthorProfile()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(
                    dest => dest.Age,
                    opt => opt.MapFrom(src => src.DateOfBirth.GetAge()));

        }
    }

}
