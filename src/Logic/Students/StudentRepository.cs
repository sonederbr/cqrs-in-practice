using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic.Students
{
    public interface IStudentRepository
    {
        List<Student> GetAll();
        void Delete(Student student);
        Student GetById(Guid id);
        void Save(Student student);
    }

    public sealed class StudentRepository : IStudentRepository
    {
        private List<Student> _students;

        public StudentRepository()
        {
            if (_students == null)
            {
                _students = new List<Student>();
                _students.Add(new Student("Ederson Lima", "ederson@gmail.com"));
                _students.Add(new Student("Joao Silva", "joao@gmail.com"));
            }
        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(Guid id)
        {
            return _students.FirstOrDefault(p => p.Id == id);
        }

        public void Save(Student student)
        {
            _students.Add(student);
        }

        public void Delete(Student student)
        {
            _students.Remove(student);
        }
    }
}
