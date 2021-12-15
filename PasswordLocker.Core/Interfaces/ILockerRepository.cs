using PasswordLocker.Core.Entities;

namespace PasswordLocker.Core.Interfaces
{
    public interface ILockerRepository : IBaseRepository<Locker>
    {
        public Locker? FindByName(string name);
        public bool DeleteByName(string name);
        public Entry? FindEntryByName(string name, string entryName);
        public bool DeleteEntryByName(string name, string entryName);
        public IEnumerable<Entry>? FindAllEntries(string name);
        public IEnumerable<Entry>? FindEntries(string name, string entryName);
    }
}
