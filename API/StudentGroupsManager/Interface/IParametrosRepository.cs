using StudentGroupsManager.Entity;

namespace StudentGroupsManager.Interface
{
    public interface IParametrosRepository : IRepository<Parametros>
    {
        bool DeadLineReachedByCourse(int courseId);
        bool DeadLineReachedByGroup(int groupId);
    }
}
