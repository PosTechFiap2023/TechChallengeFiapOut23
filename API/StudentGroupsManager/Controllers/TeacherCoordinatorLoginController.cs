using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.DTO;
using StudentGroupsManager.Interface;
using StudentGroupsManager.Services;

namespace StudentGroupsManager.Controllers
{
    [ApiController]
    [Route("login/teacher")]
    public class TeacherCoordinatorLoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ITeacherCoordinatorRepository _teacherCoordinatorRepository;
        public TeacherCoordinatorLoginController(ITokenService tokenService, ITeacherCoordinatorRepository teacherCoordinatorRepository)
        {
            _tokenService = tokenService;
            _teacherCoordinatorRepository = teacherCoordinatorRepository;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] TeacherCoordinatorLoginDTO loginDto)
        {
            var teacherCoordinator = _teacherCoordinatorRepository.GetByRMPassword(loginDto.RP, loginDto.Password);

            if (teacherCoordinator == null)
                return NotFound(new { msg = "RP ou senha inválidos" });

            var token = _tokenService.GenerateTokenTeacherCoordinator(teacherCoordinator);
            teacherCoordinator.Password = "";

            return Ok(new
            {
                TeacherCoordinators = teacherCoordinator,
                Toke = token

            });
        }

    }
}
