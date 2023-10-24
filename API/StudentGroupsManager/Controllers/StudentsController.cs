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
    //[Authorize(Roles = "Student")]
    public class StudentsController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IStudentRepository _studentRepository;

        public StudentsController(ITokenService tokenService, IStudentRepository studentRepository)
        {
            _tokenService = tokenService;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Obtem Lista de estudantes 
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
        // GET: api/Students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(_studentRepository.GetAll());
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
        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            return Ok(_studentRepository.GetById(id));
        }

        /// <summary>
        /// Atualiza um estudante
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
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public ActionResult PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _studentRepository.Update(student);
            return Ok();
        }

        /// <summary>
        /// Inclui um estudante
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
        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public ActionResult<Student> PostStudent(Student student)
        {
            if (_studentRepository == null)
            {
                return Problem("Entity set 'StudentGroupsManagerContext.Students'  is null.");
            }
            _studentRepository.Insert(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        /// <summary>
        /// Exclui um estudante
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
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(Student student)
        {
            if (_studentRepository == null)
            {
                return NotFound();
            }
            var T = _studentRepository.GetById(student.Id);
            if (T == null)
            {
                return NotFound();
            }

            _studentRepository.Delete(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }
    }
}