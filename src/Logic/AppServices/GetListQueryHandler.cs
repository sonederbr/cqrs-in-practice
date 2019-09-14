using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Students;

namespace Logic.AppServices
{
    public sealed class GetListQuery : IQuery<List<StudentDto>>
    {
        public string EnrolledIn { get; }
        public int? NumberOfCourses { get; }

        public GetListQuery(string enrolledIn, int? numberOfCourses)
        {
            EnrolledIn = enrolledIn;
            NumberOfCourses = numberOfCourses;
        }
    }

    public sealed class GetListQueryHandler : IQueryHandler<GetListQuery, List<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        public GetListQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<StudentDto> Handle(GetListQuery query)
        {
            IReadOnlyList<Student> students = _studentRepository.GetAll();
            var dtos = students.Select(p => ConvertToDto(p)).ToList();
            return dtos;
        }

        private StudentDto ConvertToDto(Student student)
        {
            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Course1 = student.FirstEnrollment?.Course?.Name,
                Course1Grade = student.FirstEnrollment?.Grade.ToString(),
                Course1Credits = student.FirstEnrollment?.Course?.Credits,
                Course2 = student.SecondEnrollment?.Course?.Name,
                Course2Grade = student.SecondEnrollment?.Grade.ToString(),
                Course2Credits = student.SecondEnrollment?.Course?.Credits,
            };
        }
    }
}
