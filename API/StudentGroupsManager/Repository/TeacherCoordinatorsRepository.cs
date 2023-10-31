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
            _context.TeacherCoordinators.Remove(GetById(entity.Id));
            _context.SaveChanges();
        }

        public TeacherCoordinator GetById(int id)
        {
            return _context.TeacherCoordinators.FirstOrDefault(s => s.Id == id);
        }

        IList<TeacherCoordinator> IRepository<TeacherCoordinator>.GetAll()
        {
            return _context.TeacherCoordinators.ToList();
        }

        public void Insert(TeacherCoordinator entity)
        {
            _context.TeacherCoordinators.Add(entity);
            _context.SaveChanges();
        }

        public void Update(TeacherCoordinator entity)
        {
            _context.TeacherCoordinators.Update(entity);
            _context.SaveChanges();
        }

        public TeacherCoordinator GetByRMPassword(string rp, string password)
        {
            return _context.TeacherCoordinators.FirstOrDefault(s => s.RP == rp && s.Password == password);
        }
    }
}