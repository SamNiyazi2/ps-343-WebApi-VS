using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using ps_343_webAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// 02/28/2022 04:22 pm - SSN - [20220228-1618] - [001] - M06-06 - Demo: Creating a collection of resources

namespace ps_343_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorCollectionsController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;
        private readonly IMapper mapper;

        public AuthorCollectionsController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {


            this.courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException($"20220228-1624:ps-343-webAPI - null [{nameof(courseLibraryRepository)}]");
            this.mapper = mapper ?? throw new ArgumentNullException($"20220228-1625:ps-343-webAPI - null [{nameof(mapper)}");
        }


        // GET: api/<AuthorCollectionController>
        [HttpPost]
        public ActionResult<IEnumerable<AuthorDTO>> CreateAuthorCollection(IEnumerable<AuthorCreateDTO> authorCollection)
        {
            var authorEntities = mapper.Map<IEnumerable<Author>>(authorCollection);

            foreach (var author in authorEntities)
            {
                courseLibraryRepository.AddAuthor(author);
            }

            courseLibraryRepository.Save();

            return Ok();

        }

    }
}
