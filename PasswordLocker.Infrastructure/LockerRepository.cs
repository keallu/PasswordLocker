using LiteDB;
using PasswordLocker.Core.Entities;
using PasswordLocker.Core.Interfaces;
using System.Text.RegularExpressions;

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

        public Entry? FindEntryByName(string name, string entryName)
        {
            Locker? locker = FindByName(name);

            return locker?.Entries.Find(o => o.Name == entryName);
        }

        public bool DeleteEntryByName(string name, string entryName)
        {
            Locker? locker = FindByName(name);

            return locker?.Entries.RemoveAll(o => o.Name == entryName) > 0 && Update(locker);
        }

        public IEnumerable<Entry>? FindAllEntries(string name)
        {
            Locker? locker = FindByName(name);

            return locker?.Entries;
        }

        public IEnumerable<Entry>? FindEntries(string name, string entryName)
        {
            Locker? locker = FindByName(name);

            return locker?.Entries.FindAll(o => Regex.IsMatch(o.Name, entryName, RegexOptions.IgnoreCase));            
        }
    }
}
