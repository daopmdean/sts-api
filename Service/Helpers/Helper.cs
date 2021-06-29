using System;
using System.Linq;

namespace Service.Helpers
{
    public class Helper
    {
        private static readonly Random random = new();

        public static string GenerateRandomPassword(int length)
        {
            const string chars = "0123456789";

            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
