using Microsoft.EntityFrameworkCore;
using StudentGroupsManager.Data;
using StudentGroupsManager.DTO;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Repository;

public class CourseGroupRepository : ICourseGroupRepository
{
    private readonly StudentGroupsManagerContext _context;

    public CourseGroupRepository(StudentGroupsManagerContext context)
    {
        _context = context;
    }

    IList<CourseGroup> IRepository<CourseGroup>.GetAll()
    {
        throw new NotImplementedException();
        // métodos necessários devido implementação do repositório padrão
    }

    public CourseGroup GetById(int id)
    {
        return _context.Groups.FirstOrDefault(x => x.Id == id);
    }

    public CourseGroup GetByRMPassword(string rm, string password)
    {
        throw new NotImplementedException();
        // métodos necessários devido implementação do repositório padrão
    }

    public IList<GroupGetDTO> GetAll()
    {
        var response = new List<GroupGetDTO>();
        var groups = _context.Groups
            .Include(x => x.Course)
            .Include(x => x.Creator).ToList();

        foreach (var group in groups)
        {
            response.Add(new GroupGetDTO()
            {
                Id = group.Id,
                Name = group.Name,
                MaxNumberOfStudents = group.MaxNumberOfStudents,
                StudentsJoined = group.StudentsJoined,
                CourseName = group.Course.NameCourse,
                CreatorName = group.Creator.Name
            });
        }

        return response;
    }

    public void Insert(CourseGroup entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public void Update(CourseGroup entity)
    {
        _context.Update(entity);
        _context.SaveChanges();
    }

    public void Delete(CourseGroup entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
    }

    public IList<GroupGetDTO> GetActiveListByCourse(int courseId)
    {
        var response = new List<GroupGetDTO>();
        var groups = _context.Groups.Where(x => x.CourseId == courseId && x.IsClosed == false)
            .Include(x => x.Course)
            .Include(x => x.Creator).ToList();

        foreach (var group in groups)
        {
            response.Add(new GroupGetDTO()
            {
                Id = group.Id,
                Name = group.Name,
                MaxNumberOfStudents = group.MaxNumberOfStudents,
                StudentsJoined = group.StudentsJoined,
                CourseName = group.Course.NameCourse,
                CreatorName = group.Creator.Name
            });
        }

        return response;
    }

    public void ChangeMaxNumberOfStudents(int id, int numberOfStudents)
    {
        if (numberOfStudents >= 6)
            throw new Exception("O número máximo permitido de estudantes por grupo é de 5 pessoas.");
        {
            
        }
        var group = GetById(id);

        if (group.StudentsJoined > numberOfStudents)
            throw new Exception(
                "O número atual de estudantes é superior ao valor máximo de participantes informado.");
        
        
        group.MaxNumberOfStudents = numberOfStudents;

        _context.Update(group);
        _context.SaveChanges();
    }

    public void EnrollAGroup(int id)
    {
        var group = GetById(id);

        if (group.IsClosed) throw new Exception("O grupo já atingiu o limite de participantes");

        group.StudentsJoined++;

        if (group.StudentsJoined >= group.MaxNumberOfStudents) group.IsClosed = true;

        _context.SaveChanges();
    }

    public void CreateAGroup(GroupCreateDTO dto)
    {
        var entity = new CourseGroup()
        {
            CourseId = dto.CourseId,
            CreatorId = dto.CreatorId,
            Name = dto.Name
        };

        if (dto.MaxNumberOfStudents is not null) entity.MaxNumberOfStudents = dto.MaxNumberOfStudents.Value;
        
        Insert(entity);
    }
}