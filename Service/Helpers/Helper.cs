using System;
using System.Collections.Generic;
using System.Linq;
using Data.Models.Requests;

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

        public static bool InTimeRange(
            DateTime time, DateTime timeRequest, int timeRange)
        {
            if (timeRequest <= time.AddMinutes(timeRange)
                && timeRequest >= time.AddMinutes(-timeRange))
            {
                return true;
            }

            return false;
        }

        public static List<string> GetUsers(IEnumerable<ShiftAssignmentInfo> assignments)
        {
            Dictionary<string, string> keyValues = new();

            foreach (var assign in assignments)
            {
                try
                {
                    keyValues.Add(assign.Username, "");
                }
                catch
                {

                }
            }
            Dictionary<string, string>.KeyCollection keys = keyValues.Keys;

            return keys.ToList();
        }
    }
}
