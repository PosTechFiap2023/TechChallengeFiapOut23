using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly StudentGroupsManagerContext _context;
        private ILogger<CoursesController> _logger;

        #region CoursesController
        public CoursesController(StudentGroupsManagerContext context,
                                    ILogger<CoursesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region GetCourses
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
          if (_context.Courses == null)
          {
                _logger.LogInformation("[CoursesController > GetCourses] Não foi encontrado Courses no contexto.");
                return NotFound();
          }
            return await _context.Courses.ToListAsync();
        }
        #endregion

        #region GetCourse
        /// <summary>
        /// Obtem curso de acordo com o id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        // GET: api/Courses/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
          if (_context.Courses == null)
          {
                _logger.LogInformation("[CoursesController > GetCourse] Não foi encontrado Courses no contexto.");
                return NotFound();
          }
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                _logger.LogInformation("[CoursesController > GetCourse] Não foi possível localizar curso com o id informado!");
                return NotFound();
            }

            return course;
        }
        #endregion

        #region PutCourse
        /// <summary>
        /// Atualiza um curso 
        /// </summary>
        /// <param name="id"></param>
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
        [Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
                _logger.LogInformation("[CoursesController > PutCourse] O id informado é diferente da entidade curso.");
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbex)
            {
                if (!CourseExists(id))
                {
                    _logger.LogInformation("[CoursesController > PutCourse] Não foi possível localizar curso com o id informado!");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"[CoursesController > PutCourse] Erro ao atualizar curso com id {id}. Erro: {dbex.Message}", dbex);
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region PostCourse
        /// <summary>
        /// Inclui um Curso
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
        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
          if (_context.Courses == null)
          {
                _logger.LogInformation("[CoursesController > PostCourse] Não foi encontrado Courses no contexto.");
                return Problem("Entity set 'StudentGroupsManagerContext.Courses'  is null.");
          }
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }
        #endregion

        #region DeleteCourse
        /// <summary>
        /// Exclui um curso de acordo com o id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Exemplo:
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        // DELETE: api/Courses/5
        [Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (_context.Courses == null)
            {
                _logger.LogInformation("[CoursesController > DeleteCourse] Não foi encontrado Courses no contexto.");
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                _logger.LogInformation("[CoursesController > DeleteCourse] Não foi possível localizar curso com o id informado!");
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region CourseExists
        private bool CourseExists(int id)
        {
            return (_context.Courses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
