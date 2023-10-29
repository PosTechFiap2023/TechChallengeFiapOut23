using Microsoft.AspNetCore.Mvc;
using StudentGroupsManager.DTO;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GroupController : ControllerBase
{
    private readonly ILogger<GroupController> _logger;
    private readonly ICourseGroupRepository _courseGroupRepository;
    private readonly IStudentGroupRepository _studentGroupRepository;

    public GroupController(ICourseGroupRepository courseGroupRepository, IStudentGroupRepository studentGroupRepository, ILogger<GroupController> logger)
    {
        _courseGroupRepository = courseGroupRepository;
        _studentGroupRepository = studentGroupRepository;
        _logger = logger;
    }

    /// <summary>
    /// Obtém lista de todos os grupos
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// GET: api/group
    [HttpGet()]
    public ActionResult<IEnumerable<GroupGetDTO>> GetAll()
        => Ok(_courseGroupRepository.GetAll());
    
    /// <summary>
    /// Obtém lista de grupos ativos por grupo
    /// </summary>
    /// <param name="courseId"></param>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// GET: api/group/1
    [HttpGet("{courseId:int}")]
    public ActionResult<IEnumerable<GroupGetDTO>> GetGroups(int courseId)
        => Ok(_courseGroupRepository.GetActiveListByCourse(courseId));
    
    /// <summary>
    /// Cria um novo grupo para um curso
    /// </summary>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// POST: api/group
    [HttpPost]
    public ActionResult CreateGroup(GroupCreateDTO dto)
    {
        _courseGroupRepository.CreateAGroup(dto);
        return Ok();
    }
        
    /// <summary>
    /// Altera o número máximo de estudantes por grupo
    /// </summary>
    /// <param name="courseId"></param>
    /// <param name="numberOfStudents"></param>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// PUT: api/group/1/3
    [HttpPut("{courseId:int}/{numberOfStudents:int}")]
    public ActionResult ChangeGroupMembersLimit(int courseId, int numberOfStudents)
    {
        _courseGroupRepository.ChangeMaxNumberOfStudents(courseId, numberOfStudents);
        return Ok();
    }  
    
    /// <summary>
    /// Permite um aluno juntar-se à um grupo
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// POST: api/group/1/3
    [HttpPost("{groupId:int}/{studentId:int}")]
    public ActionResult EnrollAGroup(int groupId, int studentId)
    {
        try
        {
            _courseGroupRepository.EnrollAGroup(groupId);
            _studentGroupRepository.EnrollStudent(groupId, studentId);
            return Ok();
        }
        catch (Exception)
        {
            _logger.LogError("Ocorreu um erro ao juntar-se ao grupo");
            return BadRequest();
        }
    }
    
    /// <summary>
    /// Permite um aluno sair de um grupo
    /// </summary>
    /// <param name="groupId"></param>
    /// <param name="studentId"></param>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// DELETE: api/group/1/3
    [HttpDelete("{groupId:int}/{studentId:int}")]
    public ActionResult UnEnrollAGroup(int groupId, int studentId)
    {
        _studentGroupRepository.UnEnrollStudent(groupId, studentId);
        return Ok();
    }
    
    /// <summary>
    /// Exclui um grupo
    /// </summary>
    /// <param name="groupId"></param>
    /// <returns></returns>
    /// <response code="200">Retorna Sucesso</response>
    /// <response code="401">Não Autenticado</response>
    /// <response code="403">Não Autorizado</response>
    /// Delete: api/group/1
    [HttpDelete("{groupId:int}")]
    public async Task<ActionResult> Delete(int groupId)
    {
        await _studentGroupRepository.UnEnrollAllStudents(groupId);
        var group = _courseGroupRepository.GetById(groupId);
        _courseGroupRepository.Delete(group);
        return Ok();
    }
}