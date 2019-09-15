using CSharpFunctionalExtensions;
using Logic.Decorators;
using Logic.Students;
using System;

namespace Logic.AppServices
{
    public sealed class EditPersonalInfoCommand : ICommand
    {
        public EditPersonalInfoCommand(Guid id, string name, string email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }

    [AuditLog]
    [DatabaseRetry]
    public sealed class EditPersonalInfoCommandHandler : ICommandHandler<EditPersonalInfoCommand>
    {
        private readonly IStudentRepository _studentRepository;
        public EditPersonalInfoCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public Result Handle(EditPersonalInfoCommand command)
        {
            var student = _studentRepository.GetById(command.Id);

            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            student.Name = command.Name;
            student.Email = command.Email;

            _studentRepository.Save(student);

            return Result.Ok();
        }
    }
}
