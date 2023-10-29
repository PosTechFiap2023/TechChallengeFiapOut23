using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Interface
{
    public interface IStudentRepository : IRepository<Student>
    {
        IList<Student> Studants();
    }
}
