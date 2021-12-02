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
        public string GeneratePassword()
        {
            return _passwordService.GeneratePassword();
        }

        public string GetPassword()
        {
            return _passwordService.GetPassword();
        }
    }
}
