using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Infrastructure.Repositories;

public class CourseGroupRepository : ICourseGroupRepository
{
    private readonly StudentGroupsManagerContext _context;

    public CourseGroupRepository(StudentGroupsManagerContext context)
    {
        _context = context;
    }
    
    public CourseGroup GetById(int id)
    {
        return _context.Groups.FirstOrDefault(x => x.Id == id);
    }

    public IList<CourseGroup> GetAll()
    {
        return _context.Groups.ToList();
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

    public IList<CourseGroup> GetActiveListByCourse(int courseId)
    {
        return _context.Groups.Where(x => x.CourseId == courseId && x.IsClosed == false).ToList();
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
}