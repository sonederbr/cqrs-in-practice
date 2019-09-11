using System;
using CSharpFunctionalExtensions;

namespace Logic.Students
{
    public interface ICommand
    {

    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }

    public sealed class EditPersonalInfoCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

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
