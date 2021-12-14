using PasswordLocker.Core.Entities;
using PasswordLocker.Core.Interfaces;

namespace PasswordLocker.Core.Services
{
    public class LockerService : ILockerService
    {
        private readonly ILockerRepository _lockerRepository;

        public LockerService(ILockerRepository lockerRepository)
        {
            _lockerRepository = lockerRepository;
        }

        public Locker Add(Locker locker)
        {
            return _lockerRepository.Create(locker);
        }

        public Locker Add(string name, string password)
        {
            return Add(new Locker { Name = name, Password = password });
        }

        public bool Update(Locker locker)
        {
            return _lockerRepository.Update(locker);
        }

        public bool Remove(int id)
        {
            return _lockerRepository.Delete(id);
        }

        public bool Remove(string name)
        {
            return _lockerRepository.DeleteByName(name);
        }

        public Locker? Find(string name)
        {
            return _lockerRepository.FindByName(name);
        }

        public IEnumerable<Locker> GetAll()
        {
            return _lockerRepository.FindAll();
        }

        public IEnumerable<Entry>? GetAllEntries(string name)
        {
            return _lockerRepository.FindAllEntries(name);
        }

        public IEnumerable<Entry>? GetEntries(string name, string entryName)
        {
            return _lockerRepository.FindEntries(name, entryName);
        }
    }
}
