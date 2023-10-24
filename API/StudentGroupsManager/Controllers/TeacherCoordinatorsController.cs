using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;
using StudentGroupsManager.Repository;
using StudentGroupsManager.Services;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "TeacherCoordinators")]
    public class TeacherCoordinatorsController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ITeacherCoordinatorRepository _teacherCoordinatorRepository;

        public TeacherCoordinatorsController(ITokenService tokenService, ITeacherCoordinatorRepository teacherCoordinatorRepository)
        {
            _tokenService = tokenService;
            _teacherCoordinatorRepository = teacherCoordinatorRepository;
        }

        /// <summary>
        /// Obtem Lista de professores e coordenadores 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        // GET: api/TeacherCoordinators
        [HttpGet]
        public ActionResult<IEnumerable<TeacherCoordinator>> GetTeacherCoordinators()
        {
            return Ok(_teacherCoordinatorRepository.GetAll());
        }

        /// <summary>
        /// Obtem 1 professor ou coordenador de acordo com o id 
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
        // GET: api/TeacherCoordinators/5
        [HttpGet("{id}")]
        public ActionResult<TeacherCoordinator> GetTeacherCoordinator(int id)
        {
            return Ok(_teacherCoordinatorRepository.GetById(id));
        }

        /// <summary>
        /// Atualiza um professor ou coordenador
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        // PUT: api/TeacherCoordinators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutTeacherCoordinator(int id, TeacherCoordinator teacherCoordinator)
        {
            if (id != teacherCoordinator.Id)
            {
                return BadRequest();
            }

            _teacherCoordinatorRepository.Update(teacherCoordinator);
            return Ok();
        }

        /// <summary>
        /// Inclui um professor ou coordenador
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        // POST: api/TeacherCoordinators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<TeacherCoordinator> PostTeacherCoordinator(TeacherCoordinator teacherCoordinator)
        {
            if (_teacherCoordinatorRepository == null)
            {
                return Problem("Entity set 'StudentGroupsManagerContext.Students'  is null.");
            }
            _teacherCoordinatorRepository.Insert(teacherCoordinator);

            return CreatedAtAction("GetTeacherCoordinator", new { id = teacherCoordinator.Id }, teacherCoordinator);
        }

        /// <summary>
        /// Exclui um professor ou coordenador
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        // DELETE: api/TeacherCoordinators/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTeacherCoordinator(TeacherCoordinator teacherCoordinator)
        {
            if (_teacherCoordinatorRepository == null)
            {
                return NotFound();
            }
            var T = _teacherCoordinatorRepository.GetById(teacherCoordinator.Id);
            if (T == null)
            {
                return NotFound();
            }

            _teacherCoordinatorRepository.Delete(teacherCoordinator);

            return CreatedAtAction("GetTeacherCoordinator", new { id = teacherCoordinator.Id }, teacherCoordinator);
        }
    }
}
