using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentGroupsManagerContext _context;
        private ILogger<StudentsController> _logger;

        #region StudentsController
        public StudentsController(StudentGroupsManagerContext context,
                                ILogger<StudentsController> logger)
        {
            _context = context;
            _logger = logger;
        }
        #endregion

        #region GetStudents
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
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
          if (_context.Students == null)
          {
              _logger.LogInformation("[StudentsController > GetStudents] Não foi encontrado Students no contexto.");
              return NotFound();
          }
            return await _context.Students.ToListAsync();
        }
        #endregion

        #region GetStudent
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
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
          if (_context.Students == null)
          {
                _logger.LogInformation("[StudentsController > GetStudent] Não foi encontrado Students no contexto.");
                return NotFound();
          }
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                _logger.LogInformation("[StudentsController > GetStudents] Não foi possível localizar estudante com o id informado!");
                return NotFound();
            }

            return student;
        }
        #endregion

        #region PutStudent
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
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                _logger.LogInformation("[StudentsController > PutStudent] O id informado é diferente da entidade estudante.");
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException dbex)
            {
                if (!StudentExists(id))
                {
                    _logger.LogInformation("[StudentsController > PutStudent] Não foi possível localizar estudante com o id informado!");
                    return NotFound();
                }
                else
                {
                    _logger.LogError($"[StudentsController > PutStudent] Erro ao atualizar estudante com id {id}. Erro: {dbex.Message}", dbex);
                    throw;
                }
            }

            return NoContent();
        }
        #endregion

        #region PostStudent
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
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
          if (_context.Students == null)
          {
                _logger.LogInformation("[StudentsController > PostStudent] Não foi encontrado Students no contexto.");
                return Problem("Entity set 'StudentGroupsManagerContext.Students'  is null.");
          }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }
        #endregion

        #region DeleteStudent
        /// <summary>
        /// Exclui um estudante de acordo com o id 
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
        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                _logger.LogInformation("[StudentsController > Students] Não foi encontrado Students no contexto.");
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                _logger.LogInformation("[StudentsController > PutStudent] Não foi possível localizar estudante com o id informado!");
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region StudentExists
        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion
    }
}
