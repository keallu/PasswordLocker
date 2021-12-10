using PasswordLocker.Core.Interfaces;

namespace PasswordLocker.CLI
{
    public class Runner
    {
        private readonly IPasswordService _passwordService;

        public Runner(IPasswordService passwordService)
        { 
            _passwordService = passwordService;
        }
        public string GeneratePassword(int letters, int digits)
        {
            return _passwordService.GeneratePassword(letters, digits);
        }

        public string GetPassword()
        {
            return _passwordService.GetPassword();
        }
    }
}
