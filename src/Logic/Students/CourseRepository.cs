using System.Collections.Generic;
using System.Linq;

namespace Logic.Students
{
    public interface ICourseRepository
    {
        Course GetByName(string name);
    }

    public sealed class CourseRepository : ICourseRepository
    {
        private readonly List<Course> _courses;
        public CourseRepository()
        {
            if (_courses == null)
            {
                _courses = new List<Course>();
                _courses.Add(new Course("Psicology", 1));
                _courses.Add(new Course("Tecnology", 2));
                _courses.Add(new Course("Mathematic", 3));
                _courses.Add(new Course("Medicine", 4));
            }
        }

        public Course GetByName(string name)
        {
            return _courses.FirstOrDefault(x => x.Name == name);
        }
    }
}
