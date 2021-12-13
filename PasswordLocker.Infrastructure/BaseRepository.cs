using LiteDB;
using PasswordLocker.Core.Interfaces;

namespace PasswordLocker.Infrastructure
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        private readonly IDbContext _context;
        private readonly ILiteCollection<T> _collection;

        protected BaseRepository(IDbContext context)
        {
            _context = context;
            _collection = _context.Database.GetCollection<T>();
        }

        public T Create(T entity)
        {
            var newId = _collection.Insert(entity);
            return _collection.FindById(newId.AsInt32);
        }

        public bool Delete(int id)
        {
            return _collection.Delete(id);
        }

        public IEnumerable<T> FindAll()
        {
            return _collection.FindAll();
        }
        
        public T? FindById(int id)
        {
            return _collection.FindById(id);
        }

        public bool Update(T entity)
        {
            return _collection.Update(entity);
        }
    }
}
