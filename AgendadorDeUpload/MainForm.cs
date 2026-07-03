using System;
using System.Windows.Forms;
using AgendadorDeUpload.Security;
using AgendadorDeUpload.Models;
using AgendadorDeUpload.Services;
using AgendadorDeUpload.Scheduling;
using AgendadorDeUpload.Forms;

namespace AgendadorDeUpload
{
    public class MainForm : Form
    {
        private NotifyIcon _trayIcon;
        private ContextMenuStrip _trayMenu;
        private BackupConfig _config;
        private Timer _scheduleTimer;

        public MainForm()
        {
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            Load += MainForm_Load;
            FormClosing += MainForm_FormClosing;
            InitializeTrayIcon();
        }

        private void InitializeTrayIcon()
        {
            _trayIcon = new NotifyIcon
            {
                Icon = System.Drawing.SystemIcons.Application,
                Visible = true,
                Text = "Agendador de Upload"
            };

            _trayMenu = new ContextMenuStrip();
            _trayMenu.Items.Add("Configurações", null, OnConfigClick);
            _trayMenu.Items.Add("Executar backup agora", null, OnRunNowClick);
            _trayMenu.Items.Add(new ToolStripSeparator());
            _trayMenu.Items.Add("Sair", null, OnExitClick);
            _trayIcon.ContextMenuStrip = _trayMenu;
            _trayIcon.DoubleClick += (s, e) => OnConfigClick(null, null);
        }

        private void ShowStatus(string message)
        {
            _trayIcon.Text = message.Length > 63 ? message.Substring(0, 60) + "..." : message;
        }

        private bool TryLoadConfig()
        {
            try
            {
                var settingsPath = SecureStorage.GetDefaultSettingsPath();
                var encrypted = SecureStorage.LoadFromFile(settingsPath);
                if (encrypted == null) return false;

                if (string.IsNullOrEmpty(AppState.MasterPassword)) return false;

                var json = SecureStorage.Decrypt(encrypted, AppState.MasterPassword);
                if (json == null) return false;

                _config = BackupConfig.FromJson(json);
                return _config != null;
            }
            catch (Exception ex)
            {
                LogService.WriteError("Erro ao carregar configurações", ex);
                return false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LogService.Write("MainForm iniciado");

            var args = Environment.GetCommandLineArgs();
            bool forceRun = args.Length > 1 && args[1].Equals("/run", StringComparison.OrdinalIgnoreCase);

            if (!TryLoadConfig())
            {
                _trayIcon.ShowBalloonTip(5000, "Erro",
                    "Falha ao carregar configurações.", ToolTipIcon.Error);
                Application.Exit();
                return;
            }

            if (forceRun)
            {
                ExecuteBackupFlow();
                return;
            }

            StartScheduler();
        }

        private void StartScheduler()
        {
            _scheduleTimer?.Stop();
            _scheduleTimer?.Dispose();

            var scheduler = new SchedulerService(_config.ScheduledTime);

            if (scheduler.ShouldRunNow() && !scheduler.HasRun())
            {
                ExecuteBackupFlow();
                return;
            }

            var scheduled = scheduler.GetScheduledDateTime();
            if (scheduled.HasValue && scheduled > DateTime.Now)
            {
                _trayIcon.ShowBalloonTip(5000, "Agendado",
                    $"Backup programado para {scheduled:dd/MM/yyyy HH:mm}.", ToolTipIcon.Info);
                ShowStatus($"Aguardando {scheduled:dd/MM HH:mm}");

                _scheduleTimer = new Timer { Interval = 30000 };
                _scheduleTimer.Tick += (s, args2) =>
                {
                    var sched = new SchedulerService(_config.ScheduledTime);
                    if (sched.ShouldRunNow() && !sched.HasRun())
                    {
                        _scheduleTimer.Stop();
                        ExecuteBackupFlow();
                    }
                };
                _scheduleTimer.Start();
            }
            else
            {
                _trayIcon.ShowBalloonTip(5000, "Configuração inválida",
                    "Verifique o horário agendado nas configurações.", ToolTipIcon.Warning);
                ShowStatus("Configuração inválida");
            }
        }

        private void ExecuteBackupFlow()
        {
            if (_config == null) return;

            LogService.Write("=== INICIANDO FLUXO DE BACKUP ===");
            _trayIcon.ShowBalloonTip(3000, "Backup", "Iniciando backup...", ToolTipIcon.Info);
            ShowStatus("Iniciando backup...");

            var backupService = new BackupService(_config);
            var fileName = backupService.GenerateBackupFileName();
            var fullPath = System.IO.Path.Combine(_config.BackupFolder, fileName);

            var result = backupService.ExecuteBackup(fullPath, msg => ShowStatus(msg));
            if (!result.Success)
            {
                LogService.WriteError("Backup falhou. Encerrando.");
                _trayIcon.ShowBalloonTip(5000, "Erro",
                    $"Falha no backup:\n{result.ErrorMessage}", ToolTipIcon.Error);
                Application.Exit();
                return;
            }

            ShowStatus("Aguardando estabilização...");
            var monitor = new FileMonitorService(fullPath, _config.StableSeconds, _config.PollIntervalMs);
            if (!monitor.WaitForStabilization((size, stable) =>
                ShowStatus(stable ? "Arquivo estável. Iniciando upload..." : $"Tamanho: {FormatSize(size)}")))
            {
                Application.Exit();
                return;
            }

            bool isMega = _config.AuthMethod == "Mega";
            ShowStatus(isMega ? "Enviando para Mega..." : "Enviando para Google Drive...");
            try
            {
                if (isMega)
                {
                    var mega = new MegaUploadService(_config.MegaEmail, _config.MegaPassword, _config.MegaFolder);
                    var link = mega.Upload(fullPath, msg => ShowStatus(msg));
                    LogService.Write($"Upload Mega concluído: {link}");
                }
                else
                {
                    var uploadService = new UploadService(
                        _config.AuthMethod,
                        _config.ServiceAccountJson,
                        _config.OAuthClientId,
                        _config.OAuthClientSecret,
                        _config.OAuthRefreshToken,
                        _config.GoogleDriveFolderId);
                    var fileId = uploadService.Upload(fullPath, msg => ShowStatus(msg));
                    LogService.Write($"Upload concluído. File ID: {fileId}");
                }
            }
            catch (Exception ex)
            {
                if (_config.DeleteOnFailure)
                {
                    try { System.IO.File.Delete(fullPath); } catch { }
                }

                LogService.WriteError("Falha no upload", ex);
                _trayIcon.ShowBalloonTip(5000, "Erro",
                    $"Falha no upload:\n{ex.Message}", ToolTipIcon.Error);
                Application.Exit();
                return;
            }

            if (_config.DeleteAfterUpload)
            {
                try { System.IO.File.Delete(fullPath); } catch { }
            }

            ShowStatus("Backup concluído!");
            LogService.Write("=== BACKUP FINALIZADO COM SUCESSO ===");

            var scheduler = new SchedulerService(_config.ScheduledTime);
            scheduler.MarkAsRun();

            _trayIcon.ShowBalloonTip(5000, "Sucesso",
                isMega ? "Backup concluído e enviado ao Mega." : "Backup concluído e enviado ao Google Drive.", ToolTipIcon.Info);

            var exitTimer = new Timer { Interval = 5000 };
            exitTimer.Tick += (s, args) =>
            {
                exitTimer.Stop();
                Application.Exit();
            };
            exitTimer.Start();
        }

        private static string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1) { order++; len /= 1024; }
            return $"{len:0.##} {sizes[order]}";
        }

        private void OnConfigClick(object sender, EventArgs e)
        {
            using (var prompt = new PasswordPromptForm())
            {
                if (prompt.ShowDialog() != DialogResult.OK) return;

                var settingsPath = SecureStorage.GetDefaultSettingsPath();
                var encrypted = SecureStorage.LoadFromFile(settingsPath);

                string json = null;
                if (encrypted != null)
                {
                    json = SecureStorage.Decrypt(encrypted, prompt.Password);
                    if (json == null)
                    {
                        MessageBox.Show("Senha incorreta.", "Erro",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                var config = json != null ? BackupConfig.FromJson(json) : new BackupConfig();
                using (var configForm = new ConfigForm(config, prompt.Password))
                {
                    configForm.ShowDialog();
                    if (configForm.DialogResult == DialogResult.OK)
                    {
                        AppState.MasterPassword = configForm.SavedPassword;
                        _config = null;
                        TryLoadConfig();
                        StartScheduler();
                    }
                }
            }
        }

        private void OnRunNowClick(object sender, EventArgs e)
        {
            if (_config == null && !TryLoadConfig())
                return;

            ExecuteBackupFlow();
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scheduleTimer?.Stop();
            _scheduleTimer?.Dispose();
            if (_trayIcon != null)
            {
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }
        }
    }
}
