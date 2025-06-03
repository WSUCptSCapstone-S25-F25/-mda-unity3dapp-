using System.IO;
using UnityEngine;

namespace LoggingComponents
{
    public static class LoggingManager
    {
        private static bool _fileLoggingEnabled = false;
        private static string _logFileName = "";
        private static StreamWriter _writer = null;

        public static void SetFileLogging(bool isEnabled, string fileNameWithoutExtension)
        {
            if (!isEnabled)
            {
                _fileLoggingEnabled = false;
                if (_writer != null)
                {
                    _writer.WriteLine($"\n=== Logging session ended at {System.DateTime.Now} ===\n");
                    _writer.Close();
                    _writer = null;
                }
                return;
            }

            if (string.IsNullOrWhiteSpace(fileNameWithoutExtension))
            {
                Debug.LogError("[LoggingManager] Cannot enable file logging: filename is empty.");
                _fileLoggingEnabled = false;
                return;
            }

            if (_fileLoggingEnabled && _writer != null && _logFileName == fileNameWithoutExtension)
                return;

            if (_writer != null)
            {
                _writer.WriteLine($"\n=== Logging session switching files. Closed old '{_logFileName}' at {System.DateTime.Now} ===\n");
                _writer.Close();
                _writer = null;
            }

            _logFileName = fileNameWithoutExtension.Trim();

            string projectRoot = Path.Combine(Application.dataPath, "..");
            string debugLogsFolder = Path.Combine(projectRoot, "DebugLogs");

            try
            {
                Directory.CreateDirectory(debugLogsFolder);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[LoggingManager] Could not create DebugLogs folder at '{debugLogsFolder}': {e}");
                _fileLoggingEnabled = false;
                return;
            }

            string fullPath = Path.Combine(debugLogsFolder, _logFileName + ".log");

            try
            {
                _writer = new StreamWriter(fullPath, true);
                _writer.AutoFlush = true;
                _fileLoggingEnabled = true;
                _writer.WriteLine($"\n=== Logging session started at {System.DateTime.Now} ===\n");
                Debug.Log($"[LoggingManager] Now writing logs to: {fullPath}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[LoggingManager] Failed to open \"{fullPath}\" for logging: {e}");
                _fileLoggingEnabled = false;
                _writer = null;
            }
        }

        public static void Log(string channelName, string message)
        {
            if (!_fileLoggingEnabled || _writer == null)
                return;

            if (LogToggles.Instance == null || !LogToggles.Instance.IsEnabled(channelName))
                return;

            string timestamp = System.DateTime.Now.ToString("HH:mm:ss.fff");
            _writer.WriteLine($"[{timestamp}] [{channelName}] {message}");
        }

        public static void CloseAll()
        {
            if (_writer != null)
            {
                _writer.WriteLine($"\n=== Logging session closed (app quitting) at {System.DateTime.Now} ===\n");
                _writer.Close();
                _writer = null;
            }
            _fileLoggingEnabled = false;
            _logFileName = "";
        }
    }
}
