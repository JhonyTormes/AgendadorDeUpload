using System;
using System.Data.SqlClient;
using System.IO;
using AgendadorDeUpload.Models;

namespace AgendadorDeUpload.Services
{
    public class BackupService
    {
        private readonly BackupConfig _config;

        public BackupService(BackupConfig config)
        {
            _config = config;
        }

        public string GenerateBackupFileName()
        {
            var random = Guid.NewGuid().ToString("N").Substring(0, 8);
            return $"backup_{_config.SqlDatabase}_{random}.bak";
        }

        public string BuildConnectionString()
        {
            if (_config.UseWindowsAuth || string.IsNullOrEmpty(_config.SqlUsername))
            {
                return $"Server={_config.SqlServer};Database={_config.SqlDatabase};Integrated Security=True;Connection Timeout=30;";
            }
            return $"Server={_config.SqlServer};Database={_config.SqlDatabase};User Id={_config.SqlUsername};Password={_config.SqlPassword};Connection Timeout=30;";
        }

        public BackupResult ExecuteBackup(string backupFullPath, Action<string> onLog = null)
        {
            var result = new BackupResult
            {
                StartTime = DateTime.Now,
                FilePath = backupFullPath,
                FileName = Path.GetFileName(backupFullPath)
            };

            try
            {
                var dir = Path.GetDirectoryName(backupFullPath);
                if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var connStr = BuildConnectionString();
                LogService.Write($"Conectando ao servidor {_config.SqlServer}...");
                onLog?.Invoke($"Conectando ao servidor {_config.SqlServer}...");

                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    var sql = $"BACKUP DATABASE [{_config.SqlDatabase}] TO DISK = N'{backupFullPath}' WITH FORMAT, INIT, NAME = N'{result.FileName}', COMPRESSION";
                    LogService.Write($"Executando: BACKUP DATABASE [{_config.SqlDatabase}]...");
                    onLog?.Invoke("Executando backup...");

                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                result.Success = true;
                result.EndTime = DateTime.Now;
                var fileInfo = new FileInfo(backupFullPath);
                if (fileInfo.Exists)
                    result.FileSizeBytes = fileInfo.Length;

                LogService.Write($"Backup concluído: {backupFullPath} ({FormatSize(result.FileSizeBytes)})");
                onLog?.Invoke($"Backup concluído: {result.FileName} ({FormatSize(result.FileSizeBytes)})");
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.EndTime = DateTime.Now;
                result.ErrorMessage = ex.Message;
                LogService.WriteError($"Falha no backup: {ex.Message}");
                onLog?.Invoke($"ERRO: {ex.Message}");
            }

            return result;
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
