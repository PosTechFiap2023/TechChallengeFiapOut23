namespace StudentGroupsManager.Interface
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        T GetByRMPassword(string rm, string password);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

        IList<T> GetAll();
    }
}
