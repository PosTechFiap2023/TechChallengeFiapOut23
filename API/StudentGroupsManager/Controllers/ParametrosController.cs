using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametrosController : ControllerBase
    {
        private readonly StudentGroupsManagerContext _context;

        public ParametrosController(StudentGroupsManagerContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtem Lista de parametros 
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
        // GET: api/Parametros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Parametros>>> GetParametros()
        {
            return await _context.Parametros.ToListAsync();
        }

        /// <summary>
        /// Obtem 1 parametros de acordo com o id 
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
        // GET: api/Parametros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Parametros>> GetParametros(int id)
        {
            var parametros = await _context.Parametros.FindAsync(id);

            if (parametros == null)
            {
                return NotFound();
            }

            return parametros;
        }

        /// <summary>
        /// Atualiza um parametro
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
        // PUT: api/Parametros/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParametros(int id, Parametros parametros)
        {
            if (id != parametros.Id)
            {
                return BadRequest();
            }

            _context.Entry(parametros).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParametrosExists(id))
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

        /// <summary>
        /// Inclui um parametro
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
        // POST: api/Parametros
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Parametros>> PostParametros(Parametros parametros)
        {
            _context.Parametros.Add(parametros);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetParametros", new { id = parametros.Id }, parametros);
        }

        /// <summary>
        /// Exclui um parametro
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
        // DELETE: api/Parametros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParametros(int id)
        {
            var parametros = await _context.Parametros.FindAsync(id);
            if (parametros == null)
            {
                return NotFound();
            }

            _context.Parametros.Remove(parametros);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParametrosExists(int id)
        {
            return _context.Parametros.Any(e => e.Id == id);
        }
    }
}
