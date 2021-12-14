namespace PasswordLocker.Core.Entities
{
    public class Entry
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Entry()
        {
            Name = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
        }
    }
}
