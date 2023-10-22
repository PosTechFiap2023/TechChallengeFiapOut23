using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Repository
{
    public class TeacherCoordinatorRepository : ITeacherCoordinatorRepository
    {
        private readonly StudentGroupsManagerContext _context;
        public TeacherCoordinatorRepository(StudentGroupsManagerContext context)
        {
            _context = context;
        }

        public void Delete(TeacherCoordinator entity)
        {
            throw new NotImplementedException();
        }

        public TeacherCoordinator GetById(int id)
        {
            throw new NotImplementedException();
        }

        public TeacherCoordinator GetByRMPassword(string rp, string password)
        {
            return _context.TeacherCoordinators.FirstOrDefault(s => s.RP == rp && s.Password == password);
        }

        public void Insert(TeacherCoordinator entity)
        {
            throw new NotImplementedException();
        }

        public IList<TeacherCoordinator> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(TeacherCoordinator entity)
        {
            throw new NotImplementedException();
        }
    }
}
