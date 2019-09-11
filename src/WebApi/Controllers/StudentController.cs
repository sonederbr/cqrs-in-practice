using System;
using CSharpFunctionalExtensions;
using Logic.Dtos;
using Logic.Students;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            return Ok(_studentRepository.GetAll());
        }
        
        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
           
            return Ok(null);
        }

        [HttpDelete("{id}")]
        public IActionResult Unregister(long id)
        {
            return Ok(null);
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(long id, [FromBody] StudentEnrollmentDto dto)
        {
            
            return Ok(null);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(
            long id, int enrollmentNumber, [FromBody] StudentTransferDto dto)
        {
           
            return Ok(null);
        }

        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(
            long id, int enrollmentNumber, [FromBody] StudentDisenrollmentDto dto)
        {
            
            return Ok(null);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInfo(Guid id, [FromBody] StudentPersonalInfoDto dto)
        {
            var command = new EditPersonalInfoCommand()
            {
                Id = id,
                Name = dto.Name,
                Email = dto.Email
            };
            var handler = new EditPersonalInfoCommandHandler(_studentRepository);
            Result result = handler.Handle(command);
            return result.IsSuccess ? Ok() : Error(result.Error);
        }
    }
}
