namespace PasswordLocker.Core.Interfaces
{
    public interface IPasswordService
    {
        public string GeneratePassword(int letters = 6, int digits = 4);
    }
}
