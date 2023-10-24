using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ParametrosController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IParametrosRepository _parametrosRepository;

        public ParametrosController(ITokenService tokenService, IParametrosRepository parametrosRepository)
        {
            _tokenService = tokenService;
            _parametrosRepository = parametrosRepository;
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
        public ActionResult<IEnumerable<Parametros>> GetParametros()
        {
            return Ok(_parametrosRepository.GetAll());
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
        public ActionResult<Parametros> GetParametros(int id)
        {
            return Ok(_parametrosRepository.GetById(id));
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
        public ActionResult PutParametros(int id, Parametros parametros)
        {
            if (id != parametros.Id)
            {
                return BadRequest();
            }

            _parametrosRepository.Update(parametros);
            return Ok();
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
            if (_parametrosRepository == null)
            {
                return Problem("Entity set 'StudentGroupsManagerContext.Parametros'  is null.");
            }
            _parametrosRepository.Insert(parametros);

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
        public async Task<IActionResult> DeleteParametros(Parametros parametros)
        {
            if (_parametrosRepository == null)
            {
                return NotFound();
            }
            var T = _parametrosRepository.GetById(parametros.Id);
            if (T == null)
            {
                return NotFound();
            }

            _parametrosRepository.Delete(parametros);

            return CreatedAtAction("GetParametros", new { id = parametros.Id }, parametros);
        }
    }
}
