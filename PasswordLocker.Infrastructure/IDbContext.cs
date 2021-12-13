using LiteDB;

namespace PasswordLocker.Infrastructure
{
    public interface IDbContext : IDisposable
    {
        public LiteDatabase Database { get; }
    }
}
