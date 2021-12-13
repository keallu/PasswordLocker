using PasswordLocker.Core.Interfaces;
using System.Text;

namespace PasswordLocker.Core.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly Random _random = new();

        public string GeneratePassword(int letters = 6, int digits = 4)
        {
            return RandomString(letters) + RandomNumber(digits);
        }

        private int RandomNumber(int length)
        {
            int min = (int)Math.Pow(10, length - 1);
            int max = (int)Math.Pow(10, length) - 1;

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
