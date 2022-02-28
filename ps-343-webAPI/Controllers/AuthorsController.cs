﻿using CourseLibrary.API.Entities;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ps_343_webAPI.Models;
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

            if (courseLibraryRepository == null)
            {
                string errorMessage = "PS-343-WebAPI-AuthorsController: Injected CourseLibaryRepository is null.";
                logger.LogCritical(errorMessage);
                throw new Exception(errorMessage);
            }
        }


        [HttpGet]
        // 02/27/2022 06:57 pm - SSN - [20220227-1856] - [001] - M04-04 - Demo: Improving action return type with ActionResult<T>
        //public IActionResult Get()
        public ActionResult<IEnumerable<AuthorDTO>> Get()
        {
            var authors = CourseLibraryRepository.GetAuthors();

            // 02/27/2022 12:54 pm - SSN - [20220227-1251] - [002] - M04-03 - Separating entity model and outer facing model
            var authorDTOs = new List<AuthorDTO>();

            foreach (Author author in authors.OrderBy(r => r.DateOfBirth))
            {
                authorDTOs.Add(AuthorDTO.GetDTO(author));
            }
            // return new JsonResult(authors);
            // return Ok(authors);
            return Ok(authorDTOs);
        }


        //[HttpGet("{authorId:guid}")]
        [HttpGet("{authorId_string}")]
        //        public IActionResult Get(Guid authorId)
        public IActionResult Get(string authorId_string)
        {
            // 02/26/2022 09:17 pm - SSN - [20220226-2114] - [001] - M03-10 - Demo - returning correct status codes

            if (!Guid.TryParse(authorId_string, out Guid authorId))
            {
                return BadRequest();
            }

            var author = CourseLibraryRepository.GetAuthor(authorId);

            if (author == null) return NotFound();

            return Ok(author);
        }


    }
}
