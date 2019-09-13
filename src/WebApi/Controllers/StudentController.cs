using System;

using Logic.Dtos;
using Logic.Students;
using Logic.Utils;

using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/students")]
    public sealed class StudentController : BaseController
    {
        private readonly Messages _messages;

        public StudentController(Messages messages)
        {
            _messages = messages;
        }


        [HttpGet]
        public IActionResult GetList(string enrolled, int? number)
        {
            return Ok(_messages.Dispatch(new GetListQuery(enrolled, number)));
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
            var command = new EditPersonalInfoCommand(id, dto.Name, dto.Email);
            var result = _messages.Dispatch(command);
            return FromResult(result);
        }
    }
}
