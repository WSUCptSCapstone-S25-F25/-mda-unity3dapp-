
using System;
using System.IO;
using UnityEngine;

namespace Logging
{
    public class Logger
    {
        private readonly string _filePath;
        private StreamWriter _writer;
        private readonly string _category;

        public Logger(string filename, string category)
        {
            _category = category;
            var root = Path.Combine(Application.dataPath, "..", "DebugLogs");
            try
            {
                if (!Directory.Exists(root))
                    Directory.CreateDirectory(root);
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Logger] Could not create DebugLogs directory '{root}': {ex}");
            }

            var timestamp = DateTime.Now.ToString("dd_HH_mm");
            _filePath = Path.Combine(root, $"{filename}_{timestamp}.log");

            try
            {
                _writer = new StreamWriter(_filePath, append: true) { AutoFlush = true };
                _writer.WriteLine($"--- Log started at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{_category}] ---");
            }
            catch (Exception ex)
            {
                Debug.LogError($"[Logger] Could not open log file '{_filePath}': {ex}");
            }
        }

        public void DebugLog(string message)
        {
            if (_writer == null)
            {
                Debug.LogWarning($"[Logger] Writer not initialized for '{_category}'. Skipping message: {message}");
                return;
            }

            var time = DateTime.Now.ToString("HH:mm:ss.fff");
            _writer.WriteLine($"{time} [{_category}] {message}");
        }

        public void Close()
        {
            if (_writer != null)
            {
                _writer.Flush();
                _writer.Close();
                _writer = null;
            }
        }
    }
}
