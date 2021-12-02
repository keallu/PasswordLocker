using PasswordLocker.Core.Interfaces;
using System.Text;

namespace PasswordLocker.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly Random _random = new();

        public string GeneratePassword()
        {
            return RandomString(6) + RandomNumber(1000, 9999);
        }
        public string GetPassword()
        {
            return "Hello World!";
        }

        private int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        private string RandomString(int length)
        {
            StringBuilder? builder = new(length);

            bool lowerCase;
            char offset;
            char character;

            // A..Z or a..z: length = 26
            int lettersOffset = 26;
            
            for (var i = 0; i < length; i++)
            {
                lowerCase = _random.Next(0, 2) == 0;

                // Upper case letters 65–90 / lower case letters 97–122
                offset = lowerCase ? 'a' : 'A';

                character = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(character);
            }

            return builder.ToString();
        }
    }
}
