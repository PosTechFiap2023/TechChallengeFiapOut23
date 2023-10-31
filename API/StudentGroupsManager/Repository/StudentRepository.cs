using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentGroupsManagerContext _context;
        public StudentRepository(StudentGroupsManagerContext context)
        {
            _context = context;
        }

        public void Delete(Student entity)
        {
            _context.Students.Remove(GetById(entity.Id));
            _context.SaveChanges();
        }

        public Student GetById(int id)
        {
            return _context.Students.FirstOrDefault(s => s.Id == id);
        }

        IList<Student> IRepository<Student>.GetAll()
        {
            return _context.Students.ToList();
        }

        public void Insert(Student entity)
        {
            _context.Students.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Student entity)
        {
            _context.Students.Update(entity);
            _context.SaveChanges();
        }

        public Student GetByRMPassword(string rm, string password)
        {
            return _context.Students.FirstOrDefault(s => s.RA == rm && s.Password == password);
        }
    }
}