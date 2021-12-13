using PasswordLocker.Core.Entities;

namespace PasswordLocker.Core.Interfaces
{
    public interface ILockerService
    {
        public Locker Add(Locker locker);
        public bool Update(Locker locker);
        public bool Remove(int id);
        public bool Remove(string name);
        public Locker? Find(string name);
        public IEnumerable<Locker> GetAll();
    }
}
