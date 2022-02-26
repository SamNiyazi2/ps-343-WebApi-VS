using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// 02/26/2022 03:38 pm - SSN - [20220226-1526] - [001] - M03-03 - Demo: Implementing the outer facing contract (Part 1)

// Create from template controller API with read/write

namespace ps_343_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        public ICourseLibraryRepository CourseLibraryRepository { get; }

        public AuthorsController(ICourseLibraryRepository courseLibraryRepository, ILogger<AuthorsController> logger)
        {
            CourseLibraryRepository = courseLibraryRepository;

            if ( courseLibraryRepository == null)
            {
                string errorMessage = "PS-343-WebAPI-AuthorsController: Injected CourseLibaryRepository is null.";
                logger.LogCritical(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        [HttpGet("/GetAuthors2")]
        [Route("GetAuthors3")]
        public IActionResult GetAuthors()
        {
            var authors = CourseLibraryRepository.GetAuthors();
            return new JsonResult(authors);
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
