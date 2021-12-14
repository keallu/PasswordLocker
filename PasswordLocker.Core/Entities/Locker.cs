namespace PasswordLocker.Core.Entities
{
    public class Locker
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Entry> Entries { get; set; }

        public Locker()
        {
            Name = string.Empty;
            Password = string.Empty;
            Entries = new List<Entry>();
        }
    }
}
