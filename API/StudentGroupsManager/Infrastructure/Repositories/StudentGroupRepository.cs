using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Infrastructure.Repositories;

public class StudentGroupRepository : IStudentGroupRepository
{
    private readonly StudentGroupsManagerContext _context;

    public StudentGroupRepository(StudentGroupsManagerContext context)
    {
        _context = context;
    }

    public IList<StudentGroup> GetListStudentsByGroup(int groupId)
    {
        return _context.StudentGroups.Where(sg => sg.GroupId == groupId).ToList();
    }

    public void EnrollStudent(int groupId, int studentId)
    {
        var entity = new StudentGroup()
        {
            GroupId = groupId,
            StudentId = studentId
        };

        _context.StudentGroups.Add(entity);
    }

    public void UnEnrollStudent(int groupId, int studentId)
    {
        var entity = GetListStudentsByGroup(groupId).FirstOrDefault(s => s.StudentId == studentId);

        if (entity != null) _context.StudentGroups.Remove(entity);
    }
}