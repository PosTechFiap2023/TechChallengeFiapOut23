using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Teacher")]
    public class TeacherCoordinatorsController : ControllerBase
    {
        private readonly StudentGroupsManagerContext _context;

        public TeacherCoordinatorsController(StudentGroupsManagerContext context)
        {
            _context = context;
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
        public async Task<ActionResult<IEnumerable<TeacherCoordinator>>> GetTeacherCoordinators()
        {
          if (_context.TeacherCoordinators == null)
          {
              return NotFound();
          }
            return await _context.TeacherCoordinators.ToListAsync();
        }

        // GET: api/TeacherCoordinators/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherCoordinator>> GetTeacherCoordinator(int id)
        {
          if (_context.TeacherCoordinators == null)
          {
              return NotFound();
          }
            var teacherCoordinator = await _context.TeacherCoordinators.FindAsync(id);

            if (teacherCoordinator == null)
            {
                return NotFound();
            }

            return teacherCoordinator;
        }

        // PUT: api/TeacherCoordinators/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacherCoordinator(int id, TeacherCoordinator teacherCoordinator)
        {
            if (id != teacherCoordinator.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacherCoordinator).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherCoordinatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TeacherCoordinators
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TeacherCoordinator>> PostTeacherCoordinator(TeacherCoordinator teacherCoordinator)
        {
          if (_context.TeacherCoordinators == null)
          {
              return Problem("Entity set 'StudentGroupsManagerContext.TeacherCoordinators'  is null.");
          }
            _context.TeacherCoordinators.Add(teacherCoordinator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTeacherCoordinator", new { id = teacherCoordinator.Id }, teacherCoordinator);
        }

        // DELETE: api/TeacherCoordinators/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacherCoordinator(int id)
        {
            if (_context.TeacherCoordinators == null)
            {
                return NotFound();
            }
            var teacherCoordinator = await _context.TeacherCoordinators.FindAsync(id);
            if (teacherCoordinator == null)
            {
                return NotFound();
            }

            _context.TeacherCoordinators.Remove(teacherCoordinator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherCoordinatorExists(int id)
        {
            return (_context.TeacherCoordinators?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
