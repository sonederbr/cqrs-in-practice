using CSharpFunctionalExtensions;
using Logic.Students;
using System;

namespace Logic.AppServices
{
    public sealed class DisenrollCommand : ICommand
    {
        public Guid Id { get; }
        public int EnrollmentNumber { get; }
        public string Comment { get; }

        public DisenrollCommand(Guid id, int enrollmentNumber, string comment)
        {
            Id = id;
            EnrollmentNumber = enrollmentNumber;
            Comment = comment;
        }
    }

    public sealed class DisenrollCommandHandler : ICommandHandler<DisenrollCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public DisenrollCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Result Handle(DisenrollCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            if (string.IsNullOrWhiteSpace(command.Comment))
                return Result.Fail("Disenrollment comment is required");

            Enrollment enrollment = student.GetEnrollment(command.EnrollmentNumber);
            if (enrollment == null)
                return Result.Fail($"No enrollment found with number '{command.EnrollmentNumber}'");

            student.RemoveEnrollment(enrollment, command.Comment);

            return Result.Ok();
        }
    }
}