using LiteDB;
using PasswordLocker.Core.Entities;
using PasswordLocker.Core.Interfaces;

namespace PasswordLocker.Infrastructure
{
    public class LockerRepository : BaseRepository<Locker>, ILockerRepository
    {
        private readonly IDbContext _context;
        private readonly ILiteCollection<Locker> _collection;

        public LockerRepository(IDbContext context) : base(context)
        {
            _context = context;
            _collection = _context.Database.GetCollection<Locker>();
        }

        public Locker? FindByName(string name)
        {
            return _collection.Find(o => o.Name == name)?.FirstOrDefault();
        }

        public bool DeleteByName(string name)
        {
            Locker? locker = FindByName(name);

            return locker != null && _collection.Delete(locker.Id);
        }

        public IEnumerable<Entry>? FindAllEntries(string name)
        {
            Locker? locker = _collection.Find(o => o.Name == name)?.FirstOrDefault();

            if (locker != null)
            {
                return locker.Entries;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Entry>? FindEntries(string name, string entryName)
        {
            Locker? locker = _collection.Find(o => o.Name == name)?.FirstOrDefault();

            if (locker != null)
            {
                return locker.Entries.FindAll(o => o.Name == entryName);
            }
            else
            {
                return null;
            }
        }
    }
}
