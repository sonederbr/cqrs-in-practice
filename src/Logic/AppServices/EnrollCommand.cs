using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Students;
using System;

namespace Logic.AppServices
{
    public sealed class EnrollCommand : ICommand
    {
        public Guid Id { get; }
        public string Course { get; }
        public string Grade { get; }

        public EnrollCommand(Guid id, string course, string grade)
        {
            Id = id;
            Course = course;
            Grade = grade;
        }
    }

    [AuditLog]
    public sealed class EnrollCommandHandler : ICommandHandler<EnrollCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        public EnrollCommandHandler(IStudentRepository studentRepository
            , ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Result Handle(EnrollCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found with Id '{command.Id}'");

            Course course = _courseRepository.GetByName(command.Course);
            if (course == null)
                return Result.Fail($"Course is incorrect: '{command.Course}'");

            bool success = Enum.TryParse(command.Grade, out Grade grade);
            if (!success)
                return Result.Fail($"Grade is incorrect: '{command.Grade}'");

            student.Enroll(course, grade);

            return Result.Ok();
        }
    }
}
