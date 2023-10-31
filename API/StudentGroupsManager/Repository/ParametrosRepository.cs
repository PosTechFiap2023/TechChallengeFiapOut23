using StudentGroupsManager.Data;
using StudentGroupsManager.Entity;
using StudentGroupsManager.Interface;

namespace StudentGroupsManager.Repository
{
    public class ParametrosRepository : IParametrosRepository
    {
        private readonly StudentGroupsManagerContext _context;
        private readonly ICourseGroupRepository _groupsRepository;
        public ParametrosRepository(ICourseGroupRepository groupsRepository, StudentGroupsManagerContext context)
        {
            _groupsRepository = groupsRepository;
            _context = context;
        }

        public bool DeadLineReachedByCourse(int courseId)
        {
            var parameter = GetAll().OrderBy(p => p.Id).LastOrDefault(p => p.CourseId == courseId);

            if (parameter is null) throw new Exception("Data de fechamento não definida para o curso.");

            return DateTime.UtcNow.AddHours(-3) >= parameter?.GroupRegistrationDeadline;
        } 
        
        public bool DeadLineReachedByGroup(int groupId)
        {
            var group = _groupsRepository.GetById(groupId);
            if (group is null) throw new Exception("Grupo não encontrado");
            
            var parameter = GetParameterByCourse(group.CourseId);
            if (parameter is null) throw new Exception("Data de fechamento não definida para o curso.");

            if (DateTime.UtcNow.AddHours(-3) >= parameter.GroupRegistrationDeadline)
            {
                _groupsRepository.CloseGroupsWhenDeadlineReached(group.CourseId);
                return true;
            }
            
            return false;
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

        private Parametros GetParameterByCourse(int courseId)
        {
            return _context.Parametros.OrderBy(p => p.Id).LastOrDefault(p => p.CourseId == courseId);
        }
    }
}
