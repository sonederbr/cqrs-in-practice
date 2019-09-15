using System;
using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Students;

namespace Logic.AppServices
{
    public sealed class RegisterCommand : ICommand
    {
        public RegisterCommand(string name, string email, string course1, string course1Grade, string course2, string course2Grade)
        {
            Name = name;
            Email = email;
            Course1 = course1;
            Course1Grade = course1Grade;
            Course2 = course2;
            Course2Grade = course2Grade;
        }

        public string Name { get; }
        public string Email { get; }
        public string Course1 { get; }
        public string Course1Grade { get; }
        public string Course2 { get; }
        public string Course2Grade { get; }
    }

    [AuditLog]
    public sealed class RegisterCommandHandler : ICommandHandler<RegisterCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        public RegisterCommandHandler(IStudentRepository studentRepository
            , ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Result Handle(RegisterCommand command)
        {
            var student = new Student(command.Name, command.Email);

            if (command.Course1 != null && command.Course1Grade != null)
            {
                Course course = _courseRepository.GetByName(command.Course1);
                student.Enroll(course, (Grade)Enum.Parse(typeof(Grade), command.Course1Grade));
            }

            if (command.Course2 != null && command.Course2Grade != null)
            {
                Course course = _courseRepository.GetByName(command.Course2);
                student.Enroll(course, (Grade)Enum.Parse(typeof(Grade), command.Course2Grade));
            }

            _studentRepository.Save(student);
            
            return Result.Ok();
        }
    }
}
