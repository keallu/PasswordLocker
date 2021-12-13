using PasswordLocker.Core.Entities;

namespace PasswordLocker.Core.Interfaces
{
    public interface ILockerRepository : IBaseRepository<Locker>
    {
        public Locker? FindByName(string name);
        public bool DeleteByName(string name);
    }
}
