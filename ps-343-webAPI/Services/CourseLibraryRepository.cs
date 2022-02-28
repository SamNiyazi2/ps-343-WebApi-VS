using CourseLibrary.API.DbContexts;
using CourseLibrary.API.Entities;
using ps_343_webAPI.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseLibrary.API.Services
{
    public class CourseLibraryRepository : ICourseLibraryRepository, IDisposable
    {
        private readonly CourseLibraryContext _context;

        public CourseLibraryRepository(CourseLibraryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddCourse(Guid authorId, Course course)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (course == null)
            {
                throw new ArgumentNullException(nameof(course));
            }
            // always set the AuthorId to the passed-in authorId
            course.AuthorId = authorId;
            _context.Courses.Add(course);
        }

        public void DeleteCourse(Course course)
        {
            _context.Courses.Remove(course);
        }

        public Course GetCourse(Guid authorId, Guid courseId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            if (courseId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(courseId));
            }

            return _context.Courses
              .Where(c => c.AuthorId == authorId && c.Id == courseId).FirstOrDefault();
        }

        public IEnumerable<Course> GetCourses(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Courses
                        .Where(c => c.AuthorId == authorId)
                        .OrderBy(c => c.Title).ToList();
        }

        public void UpdateCourse(Course course)
        {
            // no code in this implementation
        }

        public void AddAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            // the repository fills the id (instead of using identity columns)
            author.Id = Guid.NewGuid();

            foreach (var course in author.Courses)
            {
                course.Id = Guid.NewGuid();
            }

            _context.Authors.Add(author);
        }

        public bool AuthorExists(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.Any(a => a.Id == authorId);
        }

        public void DeleteAuthor(Author author)
        {
            if (author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Remove(author);
        }

        public Author GetAuthor(Guid authorId)
        {
            if (authorId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(authorId));
            }

            return _context.Authors.FirstOrDefault(a => a.Id == authorId);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.ToList<Author>();
        }

        public IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds)
        {
            if (authorIds == null)
            {
                throw new ArgumentNullException(nameof(authorIds));
            }

            return _context.Authors.Where(a => authorIds.Contains(a.Id))
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }


        // 02/28/2022 07:43 am - SSN - [20220228-0739] - [001] - M05-04 - Demo: Filter resource collection
        // Add mainCategory filter
        // 02/28/2022 08:12 am - SSN - [20220228-0807] - [001] - M05-05 - Demo: Searching through resource collection
        // Add searchQuery
        // 02/28/2022 10:04 am - SSN - [20220228-0958] - [004] - M05-07 - Demo: Group action parameters together into one object
        //public IEnumerable<Author> GetAuthors(string mainCategory, string searchQuery)
        public IEnumerable<Author> GetAuthors(AuthorResourceParameters authorResourceParameters)
        {
            if (authorResourceParameters == null)
            {
                throw new ArgumentNullException($"20220228-1005-ps-343-webAPI {nameof(authorResourceParameters)}");
            }

            if (string.IsNullOrWhiteSpace(authorResourceParameters.MainCategory) && string.IsNullOrWhiteSpace(authorResourceParameters.SearchQuery))
            {
                return GetAuthors();
            }

            var collection = _context.Authors as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(authorResourceParameters.MainCategory))
            {
                authorResourceParameters.MainCategory = authorResourceParameters.MainCategory.Trim();
                collection = collection.Where(r => r.MainCategory.ToUpper() == authorResourceParameters.MainCategory.ToUpper());
            }

            if (!string.IsNullOrWhiteSpace(authorResourceParameters.SearchQuery))
            {
                authorResourceParameters.SearchQuery = authorResourceParameters.SearchQuery.Trim();

                collection = collection.Where(r =>
                        r.LastName.ToUpper().Contains(authorResourceParameters.SearchQuery.ToUpper())
                        ||
                        r.FirstName.ToUpper().Contains(authorResourceParameters.SearchQuery.ToUpper())
                        ||
                        authorResourceParameters.SearchQuery.ToUpper().Contains(r.FirstName.ToUpper())
                        ||
                        authorResourceParameters.SearchQuery.ToUpper().Contains(r.LastName.ToUpper())

                );
            }

            return collection
                .OrderBy(a => a.FirstName)
                .OrderBy(a => a.LastName)
                .ToList();
        }


        public void UpdateAuthor(Author author)
        {
            // no code in this implementation
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
