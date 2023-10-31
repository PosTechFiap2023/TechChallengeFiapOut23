using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "TeacherCoordinators")]
    public class CoursesController : ControllerBase
    {
        private ILogger<StudentsController> _logger;
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ICourseRepository courseRepository, ILogger<StudentsController> logger)
        {
            _courseRepository = courseRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtem Lista de cursos 
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
        // GET: api/Courses
        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return Ok(_courseRepository.GetAll());
        }

        /// <summary>
        /// Obtem 1 curso de acordo com o id 
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
        // GET: api/Courses/5
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null)
            {
                _logger.LogInformation("Não foi encontrado curso com o id informado!");
                return NotFound();
            }
            return Ok(course);
        }

        /// <summary>
        /// Atualiza um curso
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
        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public ActionResult PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                _logger.LogInformation("Requisição Inválida");
                return BadRequest();
            }

            _courseRepository.Update(course);
            return Ok();
        }

        /// <summary>
        /// Inclui um curso
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
        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult<Course> PostCourse(Course course)
        {
            if (_courseRepository == null)
            {
                return Problem("Entity set 'StudentGroupsManagerContext.Courses'  is null.");
            }
            _courseRepository.Insert(course);

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        /// <summary>
        /// Exclui um curso
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
        // DELETE: api/Courses/5
        //[Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public ActionResult DeleteCourse(Course course)
        {
            if (_courseRepository == null)
            {
                return NotFound();
            }
            var T = _courseRepository.GetById(course.Id);
            if (T == null)
            {
                _logger.LogInformation("Curso não encontrado ao deletar");
                return NotFound();
            }

            _courseRepository.Delete(course);

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }
    }
}
