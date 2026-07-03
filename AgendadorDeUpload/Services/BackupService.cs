using System;
using System.Data.SqlClient;
using System.IO;
using System.Threading;
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
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var customName = string.IsNullOrWhiteSpace(_config.BackupFileName) ? "" : $" {_config.BackupFileName.Trim()}";
            var dbName = _config.SqlDatabase ?? "backup";
            return $"{dbName}{customName} {date} {random}.bak";
        }

        public string BuildConnectionString()
        {
            if (_config.UseWindowsAuth || string.IsNullOrEmpty(_config.SqlUsername))
            {
                return $"Server={_config.SqlServer};Database={_config.SqlDatabase};Integrated Security=True;Connection Timeout=30;";
            }
            return $"Server={_config.SqlServer};Database={_config.SqlDatabase};User Id={_config.SqlUsername};Password={_config.SqlPassword};Connection Timeout=30;";
        }

        public bool IsBackupRunning()
        {
            try
            {
                var connStr = BuildConnectionString();
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(@"
                        SELECT COUNT(*) FROM sys.dm_exec_requests r
                        CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) t
                        WHERE r.command = 'BACKUP DATABASE'
                          AND t.text LIKE N'%BACKUP DATABASE [' + @db + N']%'
                          AND r.status NOT IN ('sleeping', 'background')", conn))
                    {
                        cmd.Parameters.AddWithValue("@db", _config.SqlDatabase);
                        return (int)cmd.ExecuteScalar() > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public void KillBackup()
        {
            try
            {
                var connStr = BuildConnectionString();
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(@"
                        DECLARE @sid INT;
                        SELECT @sid = r.session_id FROM sys.dm_exec_requests r
                        CROSS APPLY sys.dm_exec_sql_text(r.sql_handle) t
                        WHERE r.command = 'BACKUP DATABASE'
                          AND t.text LIKE N'%BACKUP DATABASE [' + @db + N']%'
                          AND r.status NOT IN ('sleeping', 'background');
                        IF @sid IS NOT NULL EXEC('KILL ' + @sid);", conn))
                    {
                        cmd.Parameters.AddWithValue("@db", _config.SqlDatabase);
                        cmd.ExecuteNonQuery();
                    }
                }
                LogService.Write("Backup cancelado via KILL.");
            }
            catch (Exception ex)
            {
                LogService.WriteError("Erro ao cancelar backup", ex);
            }
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
