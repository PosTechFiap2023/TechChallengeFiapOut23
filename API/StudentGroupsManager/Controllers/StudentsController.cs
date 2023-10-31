using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Student")]
    public class StudentsController : ControllerBase
    {
        private ILogger<StudentsController> _logger;
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository, ILogger<StudentsController> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
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
        //[Authorize]
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
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<Student> GetStudent(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null)
            {
                _logger.LogInformation("Não foi encontrado estudente com o id informado!");
                return NotFound();
            }
            return Ok(student);
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
        //[Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public ActionResult PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                _logger.LogInformation("Requisição Inválida");
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
        //[Authorize(Roles = "Teacher")]
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
        //[Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(Student student)
        {
            if (_studentRepository == null)
            {
                return NotFound();
            }
            var T = _studentRepository.GetById(student.Id);
            if (T == null)
            {
                _logger.LogInformation("Estudente não encontrado ao deletar");
                return NotFound();
            }

            _studentRepository.Delete(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }
    }
}