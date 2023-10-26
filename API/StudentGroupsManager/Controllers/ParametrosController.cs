using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "TeacherCoordinators")]
    public class ParametrosController : ControllerBase
    {
        private ILogger<StudentsController> _logger;
        private readonly IParametrosRepository _parametrosRepository;

        public ParametrosController(IParametrosRepository parametrosRepository, ILogger<StudentsController> logger)
        {
            _parametrosRepository = parametrosRepository;
            _logger = logger;
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
        //[Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Parametros>> GetParametros()
        {
            return Ok(_parametrosRepository.GetAll());
        }

        /// <summary>
        /// Obtem 1 parametro de acordo com o id 
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
        // GET: api/Parametro/5
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult<Parametros> GetParametro(int id)
        {
            var parametro = _parametrosRepository.GetById(id);
            if (parametro == null)
            {
                _logger.LogInformation("Não foi encontrado parametro com o id informado!");
                return NotFound();
            }
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
        //[Authorize(Roles = "Teacher")]
        [HttpPut("{id}")]
        public ActionResult PutParametros(int id, Parametros parametros)
        {
            if (id != parametros.Id)
            {
                _logger.LogInformation("Requisição Inválida");
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
        //[Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult<Parametros> PostParametros(Parametros parametros)
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
        //[Authorize(Roles = "Teacher")]
        [HttpDelete("{id}")]
        public ActionResult DeleteParametros(Parametros parametros)
        {
            if (_parametrosRepository == null)
            {
                return NotFound();
            }
            var T = _parametrosRepository.GetById(parametros.Id);
            if (T == null)
            {
                _logger.LogInformation("Parametro não encontrado ao deletar");
                return NotFound();
            }

            _parametrosRepository.Delete(parametros);

            return CreatedAtAction("GetParametros", new { id = parametros.Id }, parametros);
        }
    }
}
