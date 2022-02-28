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

// 02/27/2022 10:44 pm - SSN - [20220227-2241] - [001] - M04-07 - Demo: Working with parent/child relationships

namespace ps_343_webAPI.Controllers
{
    [Route("api/authors/{authorId_string}/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseLibraryRepository courseLibraryRepository;
        private readonly IMapper mapper;

        public CourseController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            this.courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException($"20220227-2248: Null [{nameof(courseLibraryRepository)}]");
            this.mapper = mapper ?? throw new ArgumentNullException($"20220227-2248: Null [{nameof(mapper)}]");
        }

        // GET: api/<CourseController>
        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> Get(string authorId_string)
        {
            Guid authorId;

            if (!Guid.TryParse(authorId_string, out authorId))
            {
                return BadRequest();
            }

            if (!courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            return Ok(mapper.Map<IEnumerable<CourseDTO>>(courseLibraryRepository.GetCourses(authorId)));
        }


        // GET: api/<CourseController>
        // 02/27/2022 11:14 pm - SSN - [20220227-2313] - [001] - M04-08 - Demo: Return a single child resource
        [Route("{courseId_string}", Name = "GetCourse")]
        [HttpGet]
        public ActionResult<CourseDTO> Get(string authorId_string, string courseId_string)
        {
            Guid authorId, courseId;

            if (!Guid.TryParse(authorId_string, out authorId))
            {
                return BadRequest();
            }

            if (!Guid.TryParse(courseId_string, out courseId))
            {
                return BadRequest();
            }

            if (!courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var course = courseLibraryRepository.GetCourse(authorId, courseId);

            if (course == null)
            {
                return NotFound();
            }


            return Ok(mapper.Map<CourseDTO>(course));
        }


        // 02/28/2022 12:51 pm - SSN - [20220228-1241] - [001] - M06-04 - Demo: Creating a child resource
        [HttpPost]
        public ActionResult<CourseDTO> CreateCourseForAuthor(string authorId_string, CourseCreateDTO newCourse)
        {
            if (!Guid.TryParse(authorId_string, out Guid authorId))
            {
                return BadRequest();
            }

            if (!courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseEntity = mapper.Map<Course>(newCourse);
            courseLibraryRepository.AddCourse(authorId, courseEntity);
            courseLibraryRepository.Save();

            var courseToReturn = mapper.Map<CourseDTO>(courseEntity);
            return CreatedAtRoute("GetCourse", new { authorId_string = authorId_string, courseId_string = courseToReturn.Id }, courseToReturn);


        }



    }
}
