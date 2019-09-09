using System.Collections.Generic;
using System.Linq;

namespace Logic.Students
{
    public sealed class CourseRepository
    {
        private List<Course> _courses;
        public CourseRepository()
        {
            if (!_courses.Any())
            {
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
