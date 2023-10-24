using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Repository
{
    public class ParametrosRepository : IParametrosRepository
    {
        private readonly StudentGroupsManagerContext _context;
        public ParametrosRepository(StudentGroupsManagerContext context)
        {
            _context = context;
        }
        public void Delete(Parametros entity)
        {
            _context.Parametros.Remove(GetById(entity.Id));
            _context.SaveChanges();
        }

        public IList<Parametros> GetAll()
        {
            return _context.Parametros.ToList();
        }

        public Parametros GetById(int id)
        {
            return _context.Parametros.FirstOrDefault(s => s.Id == id);
        }

        public void Insert(Parametros entity)
        {
            _context.Parametros.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Parametros entity)
        {
            _context.Parametros.Update(entity);
            _context.SaveChanges();
        }
        public Parametros GetByRMPassword(string rm, string password)
        {
            throw new NotImplementedException();
        }
    }
}
