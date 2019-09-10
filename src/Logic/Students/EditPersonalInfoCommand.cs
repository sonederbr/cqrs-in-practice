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
        public Result Handle(EditPersonalInfoCommand command)
        {
            var reposiotry = new StudentRepository();
            var student = reposiotry.GetById(command.Id);

            if (student == null)
                return Result.Fail($"No student found for Id {command.Id}");

            student.Name = command.Name;
            student.Email = command.Email;

            reposiotry.Save(student);

            return Result.Ok();
        }
    }
}
