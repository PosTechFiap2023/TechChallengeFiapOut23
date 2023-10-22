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
            throw new NotImplementedException();
        }

        public Student GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetByRMPassword(string rm, string password)
        {
            return _context.Students.FirstOrDefault(s => s.RA == rm && s.Password == password);
        }

        public void Insert(Student entity)
        {
            throw new NotImplementedException();
        }

        public IList<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Student entity)
        {
            throw new NotImplementedException();
        }
    }
}
