using System;
using Dummiesman;

namespace Logging
{
    public static class LoggingUtility
    {
        public static string MakeMessageTimeDiff(DateTime startTime, DateTime endTime, string message)
        {
            TimeSpan diff = endTime - startTime;
            string formattedDiff = diff.ToString(@"hh\:mm\:ss");
            string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return $"{now}: {message} took {formattedDiff}";
        }
        
        public static string MakeMessage(string message)
                {
                    string now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    return $"{now}: {message}";
                }
    }
}