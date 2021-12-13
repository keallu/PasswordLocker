using LiteDB;

namespace PasswordLocker.Infrastructure
{
    public class DbContext : IDbContext
    {
        private readonly LiteDatabase _database;
        private bool _disposed;

        public DbContext(string connectionString)
        {
            _database = new LiteDatabase(connectionString);
        }

        public LiteDatabase Database => _database;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_database != null)
                    {
                        _database.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}
