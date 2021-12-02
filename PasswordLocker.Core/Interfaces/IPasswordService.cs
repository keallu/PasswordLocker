namespace PasswordLocker.Core.Interfaces
{
    public interface IPasswordService
    {
        string GeneratePassword();
        string GetPassword();

    }
}
