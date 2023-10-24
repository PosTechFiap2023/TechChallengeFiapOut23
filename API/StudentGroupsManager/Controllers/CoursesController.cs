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
    public class CoursesController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ICourseRepository _courseRepository;

        public CoursesController(ITokenService tokenService, ICourseRepository courseRepository)
        {
            _tokenService = tokenService;
            _courseRepository = courseRepository;
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
        [HttpGet("{id}")]
        public ActionResult<Course> GetCourse(int id)
        {
            return Ok(_courseRepository.GetById(id));
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
        [HttpPut("{id}")]
        public ActionResult PutCourse(int id, Course course)
        {
            if (id != course.Id)
            {
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
                return NotFound();
            }

            _courseRepository.Delete(course);

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }
    }
}
