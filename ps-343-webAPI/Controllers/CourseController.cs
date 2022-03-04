using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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


        // 03/01/2022 05:03 pm - SSN - [20220301-1703] - [001] - M08-03 - Demo: Updating a resource (Part1)
        [HttpPut("{courseId_string}")]
        public IActionResult UpdateCourse(string authorId_string, string courseId_string, CourseUpdateDTO updatedCourse)
        {

            var controllerContext = ControllerContext;


            if (string.IsNullOrWhiteSpace(authorId_string) || string.IsNullOrWhiteSpace(courseId_string))
            {
                return BadRequest();
            }

            if (!Guid.TryParse(authorId_string, out Guid authorId) || !Guid.TryParse(courseId_string, out Guid courseId))
            {
                return BadRequest();
            }

            if (!courseLibraryRepository.AuthorExists(authorId))
            {
                return BadRequest();
            }

            var courseEntity = courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseEntity == null)
            {
                // 03/02/2022 09:55 am - SSN - [20220302-0947] - [001] - M08-09 - Demo: Updating with PUT
                // return NotFound();
                var newCourse = mapper.Map<Course>(updatedCourse);
                newCourse.Id = courseId;
                courseLibraryRepository.AddCourse(authorId, newCourse);
                courseLibraryRepository.Save();

                var courseToReturn = mapper.Map<CourseDTO>(newCourse);

                return CreatedAtRoute("GetCourse",
                    // new { authorId_string = authorId_string, courseId = courseToReturn.Id },
                    new { authorId_string, courseId_string = courseToReturn.Id },
                    courseToReturn);

            }

            mapper.Map(updatedCourse, courseEntity);

            courseLibraryRepository.UpdateCourse(courseEntity);
            courseLibraryRepository.Save();

            return NoContent();


        }



        // 03/02/2022 11:23 am - SSN - [20220302-1116] - [001] - M08-11 -Demo:  Partially updating a resource
        [HttpPatch("{courseId_string}")]
        public IActionResult PartiallyUpdateCourse(string authorId_string, string courseId_string,
                                                        JsonPatchDocument<CourseUpdateDTO> patchDocument)
        {

            if (patchDocument.Operations.Count == 0)
            {
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(authorId_string) || string.IsNullOrWhiteSpace(courseId_string))
            {
                return BadRequest();
            }

            if (!Guid.TryParse(authorId_string, out Guid authorId) || !courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }


            if (!Guid.TryParse(courseId_string, out Guid courseId))
            {
                return BadRequest();
            }

            var courseEntity = courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseEntity == null)
            {
                return NotFound();
            }


            var courseToPatch = mapper.Map<CourseUpdateDTO>(courseEntity);

            patchDocument.ApplyTo(courseToPatch, ModelState);

            var controllerContext = ControllerContext;


            // 03/03/2022 03:17 pm - SSN - [20220303-1512] - [001] - M08-12 - Demo: validating input when updating a resource with PATCH
            if (!TryValidateModel(courseToPatch))
            {
                return ValidationProblem(ModelState);
            }


            mapper.Map(courseToPatch, courseEntity);

            courseLibraryRepository.UpdateCourse(courseEntity);

            courseLibraryRepository.Save();

            return NoContent();

        }




        // 03/03/2022 03:48 pm - SSN - [20220303-1543] - [001] - M08-13 - Demo: Returning ValidationProblems from controller actions

        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }


    }

}
