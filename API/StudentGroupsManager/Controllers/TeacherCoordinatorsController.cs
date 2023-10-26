using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "TeacherCoordinators")]
    public class TeacherCoordinatorsController : ControllerBase
    {
        private ILogger<StudentsController> _logger;
        private readonly ITeacherCoordinatorRepository _teacherCoordinatorRepository;

        public TeacherCoordinatorsController(ITeacherCoordinatorRepository teacherCoordinatorRepository, ILogger<StudentsController> logger)
        {
            _teacherCoordinatorRepository = teacherCoordinatorRepository;
            _logger = logger;
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
        //[Authorize]
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
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<TeacherCoordinator> GetTeacherCoordinator(int id)
        {
            var teacherCoordinator = _teacherCoordinatorRepository.GetById(id);
            if (teacherCoordinator == null)
            {
                _logger.LogInformation("Não foi encontrado professor ou coordenador com o id informado!");
                return NotFound();
            }
            return Ok(teacherCoordinator);
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
        //[Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public ActionResult PutTeacherCoordinator(int id, TeacherCoordinator teacherCoordinator)
        {
            if (id != teacherCoordinator.Id)
            {
                _logger.LogInformation("Requisição Inválida");
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
        //[Authorize(Roles = "Teacher")]
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
        //[Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public ActionResult DeleteTeacherCoordinator(TeacherCoordinator teacherCoordinator)
        {
            if (_teacherCoordinatorRepository == null)
            {
                return NotFound();
            }
            var T = _teacherCoordinatorRepository.GetById(teacherCoordinator.Id);
            if (T == null)
            {
                _logger.LogInformation("Professor ou Coordenador não encontrado ao deletar");
                return NotFound();
            }

            _teacherCoordinatorRepository.Delete(teacherCoordinator);

            return CreatedAtAction("GetTeacherCoordinator", new { id = teacherCoordinator.Id }, teacherCoordinator);
        }
    }
}
