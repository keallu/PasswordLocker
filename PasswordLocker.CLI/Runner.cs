using PasswordLocker.Core.Entities;
using PasswordLocker.Core.Interfaces;

namespace PasswordLocker.CLI
{
    public class Runner
    {
        private readonly IPasswordService _passwordService;
        private readonly ILockerService _lockerService;

        public Runner(IPasswordService passwordService, ILockerService lockerService)
        { 
            _passwordService = passwordService;
            _lockerService = lockerService;
        }

        public string GeneratePassword(int letters, int digits)
        {
            return _passwordService.GeneratePassword(letters, digits);
        }

        public Locker Add(Locker locker)
        {
            return _lockerService.Add(locker);
        }
        public bool Update(Locker locker)
        {
            return _lockerService.Update(locker);
        }

        public bool Remove(string name)
        {
            return _lockerService.Remove(name);
        }

        public Locker? Find(string name)
        {
            return _lockerService.Find(name);
        }

        public IEnumerable<Locker> GetAll()
        {
            return _lockerService.GetAll();
        }
    }
}
