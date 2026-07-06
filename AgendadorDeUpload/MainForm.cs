using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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
        private System.Windows.Forms.Timer _scheduleTimer;
        private ToolStripMenuItem _menuCancel;
        private ToolStripMenuItem _menuRunNow;
        private bool _configOpen;

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
                Icon = LoadAppIcon(),
                Visible = true,
                Text = "Agendador de Upload"
            };

            _trayMenu = new ContextMenuStrip();
            _trayMenu.ShowImageMargin = false;
            _trayMenu.ShowCheckMargin = false;
            _trayMenu.Items.Add("Configurações", null, OnConfigClick);
            _menuRunNow = new ToolStripMenuItem("Executar upload agora", null, OnRunNowClick);
            _trayMenu.Items.Add(_menuRunNow);
            _menuCancel = new ToolStripMenuItem("Cancelar", null, OnCancelClick) { Enabled = false };
            _trayMenu.Items.Add(_menuCancel);
            _trayMenu.Items.Add(new ToolStripSeparator());
            _trayMenu.Items.Add("Sair", null, OnExitClick);
            _trayIcon.ContextMenuStrip = _trayMenu;
            _trayIcon.DoubleClick += (s, e) => OnConfigClick(null, null);
        }

        internal static Icon LoadAppIcon()
        {
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                using (var stream = assembly.GetManifestResourceStream("AgendadorDeUpload.envio.ico"))
                {
                    if (stream != null)
                        return new Icon(stream);
                }
            }
            catch { }
            return SystemIcons.Application;
        }

        private void ShowStatus(string message)
        {
            _trayIcon.Text = message.Length > 63 ? message.Substring(0, 60) + "..." : message;
        }

        private bool TryLoadConfig()
        {
            try
            {
                if (AppState.CachedConfigJson != null)
                {
                    _config = BackupConfig.FromJson(AppState.CachedConfigJson);
                    AppState.CachedConfigJson = null;
                    return _config != null;
                }

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

            SanitizeForCurrentMachine();

            if (forceRun)
            {
                _ = ExecuteBackupFlow();
                return;
            }

            var sched = new SchedulerService(_config.ScheduledTime);
            if (sched.HasFailed())
            {
                _trayIcon.ShowBalloonTip(5000, "Falha anterior",
                    "O upload agendado falhou na última execução. O agendamento foi cancelado.", ToolTipIcon.Warning);
                ShowStatus("Falha anterior — aguardando revisão");
                sched.ClearMarker();
                _config.ScheduledTime = "";
                try
                {
                    var json = _config.ToJson();
                    var encrypted = SecureStorage.Encrypt(json, AppState.MasterPassword);
                    SecureStorage.SaveToFile(SecureStorage.GetDefaultSettingsPath(), encrypted);
                }
                catch { }
                return;
            }

            StartScheduler();

            _trayIcon.ShowBalloonTip(3000, "Agendador de Upload",
                "Aplicativo rodando na bandeja.", ToolTipIcon.Info);
        }

        private void SanitizeForCurrentMachine()
        {
            var markerDir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "AgendadorDeUpload");
            var markerPath = Path.Combine(markerDir, "machine.marker");

            string currentMachineId;
            if (File.Exists(markerPath))
            {
                currentMachineId = File.ReadAllText(markerPath).Trim();
            }
            else
            {
                currentMachineId = Guid.NewGuid().ToString("N");
                if (!Directory.Exists(markerDir))
                    Directory.CreateDirectory(markerDir);
                File.WriteAllText(markerPath, currentMachineId);
            }

            if (string.IsNullOrEmpty(_config.MachineId))
            {
                _config.MachineId = currentMachineId;
                SaveCurrentConfig();
                return;
            }

            if (_config.MachineId != currentMachineId)
            {
                LogService.Write("Config detectada de outra máquina. Zerando campos locais.");
                _config.UploadFilePath = "";
                _config.SqlServer = "";
                _config.SqlDatabase = "";
                _config.SqlUsername = "";
                _config.SqlPassword = "";
                _config.BackupFolder = "";
                _config.BackupFileName = "";
                _config.MachineId = currentMachineId;
                SaveCurrentConfig();
            }
        }

        private void SaveCurrentConfig()
        {
            try
            {
                var json = _config.ToJson();
                var encrypted = SecureStorage.Encrypt(json, AppState.MasterPassword);
                SecureStorage.SaveToFile(SecureStorage.GetDefaultSettingsPath(), encrypted);
            }
            catch { }
        }

        private void StartScheduler()
        {
            _scheduleTimer?.Stop();
            _scheduleTimer?.Dispose();

            var scheduler = new SchedulerService(_config.ScheduledTime);

            if (scheduler.ShouldRunNow() && !scheduler.HasRun() && !AppState.IsRunning)
            {
                _ = ExecuteBackupFlow();
                return;
            }

            var scheduled = scheduler.GetScheduledDateTime();
            if (scheduled.HasValue && scheduled > DateTime.Now)
            {
                _trayIcon.ShowBalloonTip(5000, "Agendado",
                    $"Upload programado para {scheduled:dd/MM/yyyy HH:mm}.", ToolTipIcon.Info);
                ShowStatus($"Aguardando {scheduled:dd/MM HH:mm}");

                _scheduleTimer = new System.Windows.Forms.Timer { Interval = 15000 };
                _scheduleTimer.Tick += (s, args2) =>
                {
                    if (AppState.IsRunning) return;
                    var sched = new SchedulerService(_config.ScheduledTime);
                    if (sched.ShouldRunNow() && !sched.HasRun())
                    {
                        _scheduleTimer.Stop();
                        _ = ExecuteBackupFlow();
                    }
                };
                _scheduleTimer.Start();
            }
            else
            {
                ShowStatus("Aguardando configuração");
            }
        }

        private void OnCancelClick(object sender, EventArgs e)
        {
            AppState.CancelSource?.Cancel();
            _menuCancel.Enabled = false;
            ShowStatus("Cancelando...");
        }

        private async Task ExecuteBackupFlow()
        {
            if (_config == null) return;
            if (AppState.IsRunning)
            {
                LogService.Write("Operação já está em execução. Ignorando.");
                _trayIcon.ShowBalloonTip(3000, "Aviso", "Já existe uma operação em andamento.", ToolTipIcon.Warning);
                return;
            }

            var isBackupMode = _config.UploadMode == "Backup";
            var filePath = isBackupMode ? null : _config.UploadFilePath;

            if (!isBackupMode && string.IsNullOrWhiteSpace(filePath))
            {
                LogService.Write("Nenhum arquivo configurado para upload.");
                return;
            }

            AppState.IsRunning = true;
            AppState.CancelSource = new CancellationTokenSource();
            var ct = AppState.CancelSource.Token;
            _menuCancel.Enabled = true;
            _menuRunNow.Enabled = false;

            try
            {
                LogService.Write("=== INICIANDO FLUXO DE UPLOAD ===");
                _trayIcon.ShowBalloonTip(3000, "Upload", "Iniciando...", ToolTipIcon.Info);
                ShowStatus("Iniciando...");

                if (isBackupMode)
                {
                    var backupService = new BackupService(_config);

                    while (backupService.IsBackupRunning())
                    {
                        LogService.Write("Backup já em andamento. Aguardando 10 minutos...");
                        ShowStatus("Backup em andamento, aguardando...");
                        await Task.Delay(600000, ct);
                    }

                    ct.ThrowIfCancellationRequested();

                    var fileName = backupService.GenerateBackupFileName();
                    filePath = System.IO.Path.Combine(_config.BackupFolder, fileName);

                    var backupTask = Task.Run(() => backupService.ExecuteBackup(filePath, msg => ShowStatus(msg)), ct);

                    while (!backupTask.IsCompleted)
                    {
                        await Task.Delay(1000);
                        if (ct.IsCancellationRequested)
                        {
                            backupService.KillBackup();
                            throw new OperationCanceledException();
                        }
                    }

                    var result = backupTask.Result;
                    if (!result.Success)
                    {
                        new SchedulerService(_config.ScheduledTime).MarkAsFailed();
                        LogService.WriteError("Backup falhou. Encerrando.");
                        _trayIcon.ShowBalloonTip(5000, "Erro",
                            $"Falha no backup:\n{result.ErrorMessage}", ToolTipIcon.Error);
                        Application.Exit();
                        return;
                    }

                    ct.ThrowIfCancellationRequested();
                    ShowStatus("Aguardando estabilização...");
                    var monitor = new FileMonitorService(filePath, _config.StableSeconds, _config.PollIntervalMs);
                    if (!monitor.WaitForStabilization((size, stable) =>
                        ShowStatus(stable ? "Arquivo estável. Iniciando upload..." : $"Tamanho: {FormatSize(size)}")))
                    {
                        Application.Exit();
                        return;
                    }
                }
                else
                {
                    if (!System.IO.File.Exists(filePath))
                    {
                        LogService.WriteError("Arquivo não encontrado: " + filePath);
                        _trayIcon.ShowBalloonTip(5000, "Erro",
                            "O arquivo configurado para upload não foi encontrado.", ToolTipIcon.Error);
                        new SchedulerService(_config.ScheduledTime).MarkAsFailed();
                        Application.Exit();
                        return;
                    }
                }

                ct.ThrowIfCancellationRequested();
                bool isMega = _config.AuthMethod == "Mega";
                ShowStatus(isMega ? "Enviando para Mega..." : "Enviando para Google Drive...");
                try
                {
                    if (isMega)
                    {
                        var mega = new MegaUploadService(_config.MegaEmail, _config.MegaPassword, _config.MegaFolder, _config.MegaFolderId);
                        var link = mega.Upload(filePath, msg => ShowStatus(msg), ct);
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
                        var fileId = uploadService.Upload(filePath, msg => ShowStatus(msg));
                        LogService.Write($"Upload concluído. File ID: {fileId}");
                    }
                }
                catch (OperationCanceledException)
                {
                    LogService.Write("Upload cancelado pelo usuário.");
                    ShowStatus("Upload cancelado");
                    if (isBackupMode && _config.DeleteOnFailure)
                    {
                        try { System.IO.File.Delete(filePath); } catch { }
                    }
                    _trayIcon.ShowBalloonTip(5000, "Cancelado", "Upload cancelado.", ToolTipIcon.Info);
                    return;
                }
                catch (Exception ex)
                {
                    if (isBackupMode && _config.DeleteOnFailure)
                    {
                        try { System.IO.File.Delete(filePath); } catch { }
                    }
                    LogService.WriteError("Falha no upload", ex);
                    _trayIcon.ShowBalloonTip(5000, "Erro",
                        $"Falha no upload:\n{ex.Message}", ToolTipIcon.Error);
                    Application.Exit();
                    return;
                }

                if (isBackupMode && _config.DeleteAfterUpload)
                {
                    try { System.IO.File.Delete(filePath); } catch { }
                }

                ShowStatus("Upload concluído!");
                LogService.Write("=== UPLOAD FINALIZADO COM SUCESSO ===");

                var scheduler = new SchedulerService(_config.ScheduledTime);
                scheduler.MarkAsRun();

                _trayIcon.ShowBalloonTip(5000, "Sucesso",
                    isMega ? "Upload concluído e enviado ao Mega." : "Upload concluído e enviado ao Google Drive.", ToolTipIcon.Info);

                if (!isBackupMode)
                {
                    _config.UploadFilePath = "";
                    SaveCurrentConfig();
                }

                var exitTimer = new System.Windows.Forms.Timer { Interval = 5000 };
                exitTimer.Tick += (s, args) =>
                {
                    exitTimer.Stop();
                    Application.Exit();
                };
                exitTimer.Start();
            }
            catch (OperationCanceledException)
            {
                LogService.Write("Fluxo de upload cancelado pelo usuário.");
                _trayIcon.ShowBalloonTip(3000, "Cancelado", "Operação cancelada.", ToolTipIcon.Info);
                ShowStatus("Cancelado");
            }
            finally
            {
                _menuCancel.Enabled = false;
                _menuRunNow.Enabled = true;
                AppState.IsRunning = false;
                AppState.CancelSource?.Dispose();
                AppState.CancelSource = null;
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

        private void OnConfigClick(object sender, EventArgs e)
        {
            if (_configOpen) return;

            if (AppState.IsRunning)
            {
                var result = MessageBox.Show("Uma operação está em andamento. Abrir configurações pode interromper o agendamento, mas a operação atual continuará. Deseja continuar?",
                    "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;
            }

            string password;
            if (AppState.LastAuthTime.HasValue && (DateTime.Now - AppState.LastAuthTime.Value).TotalMinutes < 1)
            {
                password = AppState.MasterPassword;
            }
            else
            {
                using (var prompt = new PasswordPromptForm())
                {
                    if (prompt.ShowDialog() != DialogResult.OK) return;
                    password = prompt.Password;
                }
            }

            var settingsPath = SecureStorage.GetDefaultSettingsPath();
            var encrypted = SecureStorage.LoadFromFile(settingsPath);

            string json = null;
            if (encrypted != null)
            {
                json = SecureStorage.Decrypt(encrypted, password);
                if (json == null)
                {
                    MessageBox.Show("Senha incorreta.", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            _configOpen = true;

            AppState.LastAuthTime = DateTime.Now;

            try
            {
                var config = json != null ? BackupConfig.FromJson(json) : new BackupConfig();
                using (var configForm = new ConfigForm(config, password))
                {
                    configForm.ShowDialog();
                    if (configForm.DialogResult == DialogResult.OK)
                    {
                        AppState.MasterPassword = configForm.SavedPassword;
                        _config = null;
                        TryLoadConfig();
                        if (!AppState.IsRunning)
                            StartScheduler();
                    }
                }
            }
            finally
            {
                _configOpen = false;
            }
        }

        private async void OnRunNowClick(object sender, EventArgs e)
        {
            if (AppState.IsRunning)
            {
                _trayIcon.ShowBalloonTip(3000, "Aviso", "Já existe uma operação em andamento.", ToolTipIcon.Warning);
                return;
            }

            string password;
            if (AppState.LastAuthTime.HasValue && (DateTime.Now - AppState.LastAuthTime.Value).TotalMinutes < 1)
            {
                password = AppState.MasterPassword;
            }
            else
            {
                using (var prompt = new PasswordPromptForm())
                {
                    if (prompt.ShowDialog() != DialogResult.OK) return;
                    var settingsPath = SecureStorage.GetDefaultSettingsPath();
                    var encrypted = SecureStorage.LoadFromFile(settingsPath);
                    if (encrypted != null)
                    {
                        var json = SecureStorage.Decrypt(encrypted, prompt.Password);
                        if (json == null)
                        {
                            MessageBox.Show("Senha incorreta.", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    AppState.MasterPassword = prompt.Password;
                    AppState.LastAuthTime = DateTime.Now;
                    password = prompt.Password;
                }
            }

            if (_config == null && !TryLoadConfig())
                return;

            _scheduleTimer?.Stop();
            _scheduleTimer?.Dispose();
            _scheduleTimer = null;

            _config.ScheduledTime = "";
            try
            {
                var json = _config.ToJson();
                var encrypted = SecureStorage.Encrypt(json, AppState.MasterPassword);
                SecureStorage.SaveToFile(SecureStorage.GetDefaultSettingsPath(), encrypted);
            }
            catch { }

            await ExecuteBackupFlow();
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
