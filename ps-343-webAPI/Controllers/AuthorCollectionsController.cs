using AutoMapper;
using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using ps_343_webAPI.Helpers;
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


        // 02/28/2022 05:09 pm - SSN - [20220228-1706] - [001] - M06-07 - Demo: Working with array keys and composite keys
        [HttpGet("({ids})", Name = "GetAuthorCollection")]

        public IActionResult GetAuthorCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {

            if (ids == null)
            {
                return BadRequest();
            }

            var authorEntities = courseLibraryRepository.GetAuthors(ids);

            if (ids.Count() != authorEntities.Count())
            {
                return NotFound();
            }

            var authorsToReturn = mapper.Map<IEnumerable<AuthorDTO>>(authorEntities);

            return Ok(authorsToReturn);

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

            var authorCollectionToReturn = mapper.Map<IEnumerable<AuthorDTO>>(authorEntities);

            var idsAsString = string.Join(",", authorCollectionToReturn.Select(a => a.Id));

            return CreatedAtRoute("GetAuthorCollection", new { ids = idsAsString }, authorCollectionToReturn);

        }

    }
}
