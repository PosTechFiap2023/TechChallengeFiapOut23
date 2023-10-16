using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Controllers
{
    [ApiController]
    [Route("StudentGroupManagerController")]
    public class StudentGroupManagerController : ControllerBase
    {
        private IStudentRepository _studentRepository;

        public StudentGroupManagerController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Obtem 1 estudante de acordo com o id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// Enviar Id para requisição
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        [HttpGet("obter-estudande-por-id/{id}")]
        public IActionResult GetStudent(int id)
        {
            return Ok(_studentRepository.GetById(id));
        }
    }
}
