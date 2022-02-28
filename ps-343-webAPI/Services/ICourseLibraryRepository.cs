using CourseLibrary.API.Entities;
using ps_343_webAPI.ResourceParameters;
using System;
using System.Collections.Generic;

namespace CourseLibrary.API.Services
{
    public interface ICourseLibraryRepository
    {
        IEnumerable<Course> GetCourses(Guid authorId);
        Course GetCourse(Guid authorId, Guid courseId);
        void AddCourse(Guid authorId, Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(Course course);
        IEnumerable<Author> GetAuthors();

        // 02/28/2022 07:51 am - SSN - [20220228-0739] - [003] - M05-04 - Demo: Filter resource collection
        // Add mainCategory
        // 02/28/2022 08:18 am - SSN - [20220228-0807] - [002] - M05-05 - Demo: Searching through resource collection
        // Add searchQUery
        // IEnumerable<Author> GetAuthors(string mainCategory, string searchQuery);
        // 02/28/2022 10:04 am - SSN - [20220228-0958] - [003] - M05-07 - Demo: Group action parameters together into one object
        IEnumerable<Author> GetAuthors(AuthorResourceParameters authorResourceParameters);

        Author GetAuthor(Guid authorId);

        IEnumerable<Author> GetAuthors(IEnumerable<Guid> authorIds);
        void AddAuthor(Author author);
        void DeleteAuthor(Author author);
        void UpdateAuthor(Author author);
        bool AuthorExists(Guid authorId);
        bool Save();
    }
}
