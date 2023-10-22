using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.DTO;
using StudentGroupsManager.Interface;
using StudentGroupsManager.Services;

namespace StudentGroupsManager.Controllers
{
    [ApiController]
    [Route("login")]
    public class StudentLoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IStudentRepository _studentRepository;
        public StudentLoginController(ITokenService tokenService, IStudentRepository studentRepository)
        {
            _tokenService = tokenService;
            _studentRepository = studentRepository;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] StudentLoginDTO loginDto)
        {
            var student = _studentRepository.GetByRMPassword(loginDto.RA, loginDto.Password);

            if (student == null)
                return NotFound(new { msg = "RA ou senha inválidos" });

            var token = _tokenService.GenerateTokenStudent(student);
            student.Password = "";

            return Ok(new
            {
                Student = student,
                Toke = token

            });
        }
    }
}
