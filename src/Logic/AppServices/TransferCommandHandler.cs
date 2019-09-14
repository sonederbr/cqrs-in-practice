using CSharpFunctionalExtensions;
using Logic.Students;
using System;

namespace Logic.AppServices
{
    public sealed class TransferCommand : ICommand
    {
        public Guid Id { get; }
        public int EnrollmentNumber { get; }
        public string Course { get; }
        public string Grade { get; }

        public TransferCommand(Guid id, int enrollmentNumber, string course, string grade)
        {
            Id = id;
            EnrollmentNumber = enrollmentNumber;
            Course = course;
            Grade = grade;
        }
    }

    public sealed class TransferCommandHandler : ICommandHandler<TransferCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        public TransferCommandHandler(IStudentRepository studentRepository
            , ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Result Handle(TransferCommand command)
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

            Enrollment enrollment = student.GetEnrollment(command.EnrollmentNumber);
            if (enrollment == null)
                return Result.Fail($"No enrollment found with number '{command.EnrollmentNumber}'");

            enrollment.Update(course, grade);

            return Result.Ok();
        }
    }
}