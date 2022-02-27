using CourseLibrary.API.Entities;
using ps_343_webAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// 02/27/2022 12:51 pm - SSN - [20220227-1251] - [001] - M04-03 - Separating entity model and outer facing model

namespace ps_343_webAPI.Models
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string MainCategory { get; set; }

        public static AuthorDTO GetDTO(Author author)
        {
            return new AuthorDTO
            {
                Id = author.Id,
                Name = $"{author.FirstName} {author.LastName}",
                Age = author.DateOfBirth.GetAge(),
                MainCategory = author.MainCategory
            };
        }
    }
}
