using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentGroupsManagerContext _context;
        public CourseRepository(StudentGroupsManagerContext context)
        {
            _context = context;
        }

        public void Delete(Course entity)
        {
            _context.Courses.Remove(GetById(entity.Id));
            _context.SaveChanges();
        }

        public Course GetById(int id)
        {
            return _context.Courses.FirstOrDefault(s => s.Id == id);
        }

        IList<Course> IRepository<Course>.GetAll()
        {
            return _context.Courses.ToList();
        }

        public void Insert(Course entity)
        {
            _context.Courses.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Course entity)
        {
            _context.Courses.Update(entity);
            _context.SaveChanges();
        }

        public Course GetByRMPassword(string rm, string password)
        {
            throw new NotImplementedException();
        }
    }
}
