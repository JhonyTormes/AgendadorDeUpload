using System;
using System.IO;

namespace AgendadorDeUpload.Services
{
    public static class LogService
    {
        private static string _logDir;

        public static void Initialize()
        {
            _logDir = Path.Combine(
                Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "Logs");
            Directory.CreateDirectory(_logDir);
        }

        public static void Write(string message)
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var line = $"[{timestamp}] {message}";

            var logFile = Path.Combine(_logDir, $"log_{DateTime.Now:yyyyMMdd}.txt");
            try
            {
                File.AppendAllText(logFile, line + Environment.NewLine);
            }
            catch { }

            System.Diagnostics.Debug.WriteLine(line);
        }

        public static void WriteError(string message, Exception ex = null)
        {
            var msg = $"ERRO: {message}";
            if (ex != null)
            {
                msg += $" | {ex.GetType().Name}: {ex.Message}";
                if (!string.IsNullOrEmpty(ex.StackTrace))
                    msg += $"\n{ex.StackTrace}";
                if (ex.InnerException != null)
                    msg += $"\nInner: {ex.InnerException.GetType().Name}: {ex.InnerException.Message}";
            }
            Write(msg);
        }
    }
}
