using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using Logic.Dtos;

namespace Logic.Students
{
    public interface ICommand
    {

    }

    public interface IQuery<TResult>
    {

    }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Result Handle(TCommand command);
    }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }

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
                Email = student.Email
            };
        }
    }

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
