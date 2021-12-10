namespace PasswordLocker.Core.Interfaces
{
    public interface IPasswordService
    {
        string GeneratePassword(int letters = 6, int digits = 4);
        string GetPassword();

    }
}
