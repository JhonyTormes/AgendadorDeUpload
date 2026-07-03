using System;
using System.IO;
using System.Threading;

namespace AgendadorDeUpload.Services
{
    public class FileMonitorService
    {
        private readonly string _filePath;
        private readonly int _stableSeconds;
        private readonly int _pollIntervalMs;

        public FileMonitorService(string filePath, int stableSeconds = 30, int pollIntervalMs = 5000)
        {
            _filePath = filePath;
            _stableSeconds = stableSeconds;
            _pollIntervalMs = pollIntervalMs;
        }

        public bool WaitForStabilization(Action<long, bool> onProgress = null)
        {
            if (!File.Exists(_filePath))
            {
                LogService.WriteError("Arquivo não encontrado para monitoramento: " + _filePath);
                return false;
            }

            long lastSize = -1;
            DateTime lastChangeTime = DateTime.MinValue;

            LogService.Write($"Monitorando arquivo: {_filePath}");

            while (true)
            {
                var currentSize = new FileInfo(_filePath).Length;

                if (currentSize != lastSize)
                {
                    lastSize = currentSize;
                    lastChangeTime = DateTime.Now;
                    onProgress?.Invoke(currentSize, false);
                    LogService.Write($"Tamanho: {FormatSize(currentSize)} (crescendo...)");
                }
                else
                {
                    var stableFor = (DateTime.Now - lastChangeTime).TotalSeconds;
                    onProgress?.Invoke(currentSize, false);

                    if (stableFor >= _stableSeconds)
                    {
                        LogService.Write($"Estabilizado por {_stableSeconds}s. Tamanho final: {FormatSize(currentSize)}");
                        onProgress?.Invoke(currentSize, true);
                        return true;
                    }
                }

                Thread.Sleep(_pollIntervalMs);
            }
        }

        private static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1) { order++; len /= 1024; }
            return $"{len:0.##} {sizes[order]}";
        }
    }
}
