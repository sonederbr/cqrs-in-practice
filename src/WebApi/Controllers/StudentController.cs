using Logic.AppServices;
using Logic.Dtos;
using Logic.Utils;
using Microsoft.AspNetCore.Mvc;
using System;

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
            var result = _messages.Dispatch(new GetListQuery(enrolled, number));
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Register([FromBody] NewStudentDto dto)
        {
            var command = new RegisterCommand(
                dto.Name, dto.Email,
                dto.Course1, dto.Course1Grade,
                dto.Course2, dto.Course2Grade);

            var result = _messages.Dispatch(command);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Unregister(Guid id)
        {
            var result = _messages.Dispatch(new UnregisterCommand(id));
            return FromResult(result);
        }

        [HttpPost("{id}/enrollments")]
        public IActionResult Enroll(Guid id, [FromBody] StudentEnrollmentDto dto)
        {
            var result = _messages.Dispatch(new EnrollCommand(id, dto.Course, dto.Grade));
            return FromResult(result);
        }

        [HttpPut("{id}/enrollments/{enrollmentNumber}")]
        public IActionResult Transfer(
            Guid id, int enrollmentNumber, [FromBody] StudentTransferDto dto)
        {
            var result = _messages.Dispatch(new TransferCommand(id, enrollmentNumber, dto.Course, dto.Grade));
            return FromResult(result);
        }

        [HttpPost("{id}/enrollments/{enrollmentNumber}/deletion")]
        public IActionResult Disenroll(
            Guid id, int enrollmentNumber, [FromBody] StudentDisenrollmentDto dto)
        {
            var result = _messages.Dispatch(new DisenrollCommand(id, enrollmentNumber, dto.Comment));
            return FromResult(result);
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
