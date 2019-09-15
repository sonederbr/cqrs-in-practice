using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Students;
using System;

namespace Logic.AppServices
{
    public sealed class UnregisterCommand : ICommand
    {
        public Guid Id { get; }

        public UnregisterCommand(Guid id)
        {
            Id = id;
        }
    }

    [AuditLog]
    public sealed class UnregisterCommandHandler : ICommandHandler<UnregisterCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        public UnregisterCommandHandler(IStudentRepository studentRepository
            , ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public Result Handle(UnregisterCommand command)
        {
            Student student = _studentRepository.GetById(command.Id);
            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            _studentRepository.Delete(student);

            return Result.Ok();
        }
    }
}
