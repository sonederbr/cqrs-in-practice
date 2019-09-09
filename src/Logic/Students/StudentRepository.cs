using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Students
{
    public sealed class StudentRepository
    {
        private List<Student> _students;

        public StudentRepository()
        {
        }

        public Student GetById(Guid id)
        {
            return _students.FirstOrDefault(p => p.Id == id);
        }

        public void Save(Student student)
        {

        }

        public void Delete(Student student)
        {
            _students.Remove(student);
        }
    }
}
