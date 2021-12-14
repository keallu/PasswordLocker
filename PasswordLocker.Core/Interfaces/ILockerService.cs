using PasswordLocker.Core.Entities;

namespace PasswordLocker.Core.Interfaces
{
    public interface ILockerService
    {
        public Locker Add(Locker locker);
        public Locker Add(string name, string password);
        public bool Update(Locker locker);
        public bool Remove(int id);
        public bool Remove(string name);
        public Locker? Find(string name);
        public IEnumerable<Locker> GetAll();
        public IEnumerable<Entry>? GetAllEntries(string name);
        public IEnumerable<Entry>? GetEntries(string name, string entryName);
    }
}
