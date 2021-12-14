using PasswordLocker.Core.Entities;

namespace PasswordLocker.Core.Interfaces
{
    public interface ILockerRepository : IBaseRepository<Locker>
    {
        public Locker? FindByName(string name);
        public bool DeleteByName(string name);
        public IEnumerable<Entry>? FindAllEntries(string name);
        public IEnumerable<Entry>? FindEntries(string name, string entryName);
    }
}
