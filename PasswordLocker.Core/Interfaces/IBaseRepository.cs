namespace PasswordLocker.Core.Interfaces
{
    public interface IBaseRepository<T>
    {
        public T Create(T entity);
        public bool Delete(int id);
        public IEnumerable<T> FindAll();
        public T? FindById(int id);
        public bool Update(T entity);
    }
}