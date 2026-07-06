using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using AgendadorDeUpload.Models;
using AgendadorDeUpload.Security;
using AgendadorDeUpload.Services;
using CG.Web.MegaApiClient;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;


namespace AgendadorDeUpload.Forms
{
    public partial class ConfigForm : Form
    {
        private readonly BackupConfig _config;
        private readonly string _existingPassword;
        private string _selectedJsonContent;
        private string _oauthRefreshToken;
        private bool _scheduleCancelled;
        private bool _jsonFileModified;
        private bool _oauthSecretModified;
        public string SavedPassword { get; private set; }

        public ConfigForm(BackupConfig config, string existingPassword)
        {
            _config = config;
            _existingPassword = existingPassword;
            InitializeComponent();
            Icon = MainForm.LoadAppIcon();
            LoadConfig();
        }

        private void LoadConfig()
        {
            txtUploadFilePath.Text = _config.UploadFilePath ?? "";

            var isBackupMode = _config.UploadMode == "Backup";
            chkBackupMode.Checked = isBackupMode;
            grpBackupSql.Visible = isBackupMode;

            txtSqlServer.Text = _config.SqlServer ?? "";
            txtSqlDatabase.Text = _config.SqlDatabase ?? "";
            txtSqlUsername.Text = _config.SqlUsername ?? "";
            txtSqlPassword.Text = _config.SqlPassword ?? "";
            chkWindowsAuth.Checked = _config.UseWindowsAuth;
            txtBackupFolder.Text = _config.BackupFolder ?? "";

            if (!string.IsNullOrEmpty(_config.ServiceAccountJsonPath))
                txtServiceAccountPath.Text = _config.ServiceAccountJsonPath;
            _selectedJsonContent = null;
            _jsonFileModified = false;

            txtOAuthClientId.Text = _config.OAuthClientId ?? "";
            txtOAuthClientSecret.Text = "";
            _oauthSecretModified = false;
            _oauthRefreshToken = _config.OAuthRefreshToken;

            txtOAuthClientSecret.TextChanged -= TxtOAuthClientSecret_TextChanged;
            txtOAuthClientSecret.TextChanged += TxtOAuthClientSecret_TextChanged;

            txtMegaEmail.Text = _config.MegaEmail ?? "";
            txtMegaPassword.Text = _config.MegaPassword ?? "";
            txtMegaFolder.Text = _config.MegaFolder ?? "";

            txtBackupFileName.Text = _config.BackupFileName ?? "";
            chkDeleteAfterUpload.Checked = _config.DeleteAfterUpload;
            chkDeleteOnFailure.Checked = _config.DeleteOnFailure;

            if (_config.AuthMethod == "Mega")
            {
                rbAuthMega.Checked = true;
                txtFolderLink.Text = _config.MegaFolderId ?? "";
            }
            else if (_config.AuthMethod == "OAuth" && !string.IsNullOrEmpty(_config.OAuthRefreshToken))
            {
                rbAuthOAuth.Checked = true;
                lblOAuthStatus.Text = "Autorizado";
                lblOAuthStatus.ForeColor = System.Drawing.Color.Green;
                txtFolderLink.Text = _config.GoogleDriveFolderId ?? "";
            }
            else
            {
                rbAuthServiceAccount.Checked = true;
                txtFolderLink.Text = _config.GoogleDriveFolderId ?? "";
            }

            bool hasSchedule = DateTime.TryParseExact(_config.ScheduledTime, "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var dt) && dt > DateTime.Now;
            if (hasSchedule)
                dtpSchedule.Value = dt;
            else
                dtpSchedule.Value = DateTime.Now.AddHours(1);

            _scheduleCancelled = !hasSchedule;
            UpdateScheduleButton();

            dtpSchedule.ValueChanged -= DtpSchedule_ValueChanged;
            dtpSchedule.ValueChanged += DtpSchedule_ValueChanged;

            txtSqlUsername.Enabled = !_config.UseWindowsAuth;
            txtSqlPassword.Enabled = !_config.UseWindowsAuth;

            UpdateAuthVisibility();
            UpdateBackupModeVisibility();
        }

        private void UpdateAuthVisibility()
        {
            bool isMega = rbAuthMega.Checked;
            bool isSA = rbAuthServiceAccount.Checked;
            bool isOAuth = rbAuthOAuth.Checked;

            lblMegaEmail.Visible = isMega;
            txtMegaEmail.Visible = isMega;
            lblMegaPassword.Visible = isMega;
            txtMegaPassword.Visible = isMega;
            btnTestMegaLogin.Visible = isMega;
            lblMegaFolder.Visible = isMega;
            txtMegaFolder.Visible = isMega;
            btnSelectMegaFolder.Visible = isMega;

            lblServiceAccount.Visible = isSA;
            txtServiceAccountPath.Visible = isSA;
            btnSelectJson.Visible = isSA;

            lblOAuthClientId.Visible = isOAuth;
            txtOAuthClientId.Visible = isOAuth;
            lblOAuthClientSecret.Visible = isOAuth;
            txtOAuthClientSecret.Visible = isOAuth;
            btnAuthorizeOAuth.Visible = isOAuth;
            lblOAuthStatus.Visible = isOAuth;
        }

        private void UpdateBackupModeVisibility()
        {
            bool isBackupMode = chkBackupMode.Checked;
            grpBackupSql.Visible = isBackupMode;
            lblUploadFile.Visible = !isBackupMode;
            txtUploadFilePath.Visible = !isBackupMode;
            btnBrowseFile.Visible = !isBackupMode;

            var btnY = isBackupMode ? 660 : 370;
            btnSave.Location = new System.Drawing.Point(btnSave.Location.X, btnY);
            btnCancel.Location = new System.Drawing.Point(btnCancel.Location.X, btnY);
            this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, isBackupMode ? 700 : 410);
        }

        private void UpdateScheduleButton()
        {
            if (_scheduleCancelled)
                btnScheduleAction.Text = "Agendar";
            else
                btnScheduleAction.Text = "Cancelar Agendamento";
            dtpSchedule.Enabled = true;
        }

        private void DtpSchedule_ValueChanged(object sender, EventArgs e)
        {
            _scheduleCancelled = true;
            UpdateScheduleButton();
        }

        private void BtnScheduleAction_Click(object sender, EventArgs e)
        {
            if (_scheduleCancelled)
            {
                if (dtpSchedule.Value <= DateTime.Now)
                {
                    MessageBox.Show(this, "Defina uma data/hora futura para agendar.",
                        "Agendar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                _scheduleCancelled = false;
                UpdateScheduleButton();
            }
            else
            {
                var result = MessageBox.Show(this, "Deseja cancelar o agendamento atual?",
                    "Cancelar Agendamento", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _scheduleCancelled = true;
                    UpdateScheduleButton();
                }
            }
        }

        private void RbAuthMethod_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAuthVisibility();
        }

        private void ChkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtSqlUsername.Enabled = !chkWindowsAuth.Checked;
            txtSqlPassword.Enabled = !chkWindowsAuth.Checked;
        }

        private void BtnBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtBackupFolder.Text = dlg.SelectedPath;
            }
        }

        private async void BtnTestFolderPerm_Click(object sender, EventArgs e)
        {
            var folder = txtBackupFolder.Text.Trim();

            if (string.IsNullOrWhiteSpace(folder))
            {
                MessageBox.Show(this, "Informe a pasta de backup primeiro.", "Testar Permissão",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTestFolderPerm.Enabled = false;
            try
            {
                try
                {
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    var testFile = Path.Combine(folder, "_perm_test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + ".tmp");
                    File.WriteAllText(testFile, "test");
                    File.Delete(testFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, $"O SQL Server não consegue escrever nesta pasta.\n\n{ex.Message}",
                        "Testar Permissão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var ok = await Task.Run(() => TestSqlFolderWriteAccess(folder));
                if (ok)
                    MessageBox.Show(this, $"O SQL Server consegue escrever na pasta.", "Testar Permissão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, $"O SQL Server não consegue escrever na pasta.", "Testar Permissão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTestFolderPerm.Enabled = true;
            }
        }

        private bool TestSqlFolderWriteAccess(string folder)
        {
            var server = txtSqlServer.Text.Trim();
            var database = txtSqlDatabase.Text.Trim();
            if (string.IsNullOrWhiteSpace(server) || string.IsNullOrWhiteSpace(database))
                return false;

            try
            {
                var builder = new SqlConnectionStringBuilder
                {
                    DataSource = server,
                    InitialCatalog = database,
                    ConnectTimeout = 5
                };

                if (chkWindowsAuth.Checked)
                    builder.IntegratedSecurity = true;
                else
                {
                    builder.UserID = txtSqlUsername.Text.Trim();
                    builder.Password = txtSqlPassword.Text;
                }

                using (var conn = new SqlConnection(builder.ConnectionString))
                {
                    conn.Open();

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"
                            DECLARE @enabled int;
                            SELECT @enabled = CAST(value_in_use AS int) FROM sys.configurations WHERE name = 'xp_cmdshell';
                            IF @enabled = 0
                            BEGIN
                                EXEC sp_configure 'show advanced options', 1; RECONFIGURE;
                                EXEC sp_configure 'xp_cmdshell', 1; RECONFIGURE;
                            END";
                        cmd.ExecuteNonQuery();
                    }

                    var testFileName = Path.Combine(folder, "_sql_perm_test_" + Guid.NewGuid().ToString("N").Substring(0, 8) + ".tmp");
                    var sqlEscapePath = testFileName.Replace("'", "''");

                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = $"EXEC xp_cmdshell 'echo test > \"{sqlEscapePath}\"'";
                        cmd.ExecuteNonQuery();
                    }

                    if (File.Exists(testFileName))
                    {
                        File.Delete(testFileName);
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        private async void BtnTestMegaLogin_Click(object sender, EventArgs e)
        {
            var email = txtMegaEmail.Text.Trim();
            var password = txtMegaPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(this, "Preencha o e-mail e senha do Mega primeiro.",
                    "Testar Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnTestMegaLogin.Enabled = false;
            btnTestMegaLogin.Text = "Testando...";
            Cursor = Cursors.WaitCursor;
            try
            {
                var ok = await Task.Run(() =>
                {
                    try
                    {
                        var client = new MegaApiClient();
                        client.Login(email, password);
                        client.Logout();
                        return true;
                    }
                    catch { return false; }
                });

                MessageBox.Show(this,
                    ok ? "Teste de conexão com o Mega funcionou." : "Falha ao autenticar no Mega. Verifique e-mail e senha.",
                    "Testar Login", MessageBoxButtons.OK, ok ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            finally
            {
                btnTestMegaLogin.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void BtnSelectJson_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Arquivo JSON|*.json" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtServiceAccountPath.Text = dlg.FileName;
                    _selectedJsonContent = System.IO.File.ReadAllText(dlg.FileName);
                    _jsonFileModified = true;
                }
            }
        }

        private async void BtnTestSqlConnection_Click(object sender, EventArgs e)
        {
            btnTestSqlConnection.Enabled = false;
            try
            {
                var result = await Task.Run(() =>
                {
                    var server = txtSqlServer.Text.Trim();
                    var database = txtSqlDatabase.Text.Trim();
                    var useWinAuth = chkWindowsAuth.Checked;
                    var user = txtSqlUsername.Text.Trim();
                    var pass = txtSqlPassword.Text;

                    var builder = new SqlConnectionStringBuilder
                    {
                        DataSource = server,
                        InitialCatalog = database,
                        ConnectTimeout = 5
                    };

                    if (useWinAuth)
                        builder.IntegratedSecurity = true;
                    else
                    {
                        builder.UserID = user;
                        builder.Password = pass;
                    }

                    using (var conn = new SqlConnection(builder.ConnectionString))
                    {
                        conn.Open();
                        return "ok";
                    }
                });

                MessageBox.Show(this, "Conexão com o banco de dados estabelecida com sucesso!",
                    "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, $"Falha na conexão:\n{ex.Message}",
                    "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro inesperado:\n{ex.Message}",
                    "Teste de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnTestSqlConnection.Enabled = true;
            }
        }

        private async void BtnSelectMegaFolder_Click(object sender, EventArgs e)
        {
            var email = txtMegaEmail.Text.Trim();
            var password = txtMegaPassword.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(this, "Preencha o e-mail e senha do Mega primeiro.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnSelectMegaFolder.Enabled = false;
            Cursor = Cursors.WaitCursor;
            var oldFolder = txtMegaFolder.Text;
            txtMegaFolder.Text = "Conectando ao Mega...";
            try
            {

                INode[] nodes = await Task.Run(async () =>
                {
                    var client = new MegaApiClient();
                    client.Login(email, password);
                    var result = client.GetNodes().ToArray();
                    client.Logout();
                    return result;
                });

                txtMegaFolder.Text = oldFolder;

                using (var picker = new MegaFolderPickerForm(nodes))
                {
                    if (picker.ShowDialog(this) == DialogResult.OK)
                    {
                        txtMegaFolder.Text = picker.SelectedFolderName;
                    }
                }
            }
            catch (Exception ex)
            {
                txtMegaFolder.Text = oldFolder;
                MessageBox.Show(this, $"Erro ao conectar no Mega:\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSelectMegaFolder.Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private async void BtnAuthorizeOAuth_Click(object sender, EventArgs e)
        {
            var clientId = txtOAuthClientId.Text.Trim();
            var clientSecret = txtOAuthClientSecret.Text.Trim();

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                MessageBox.Show(this, "Informe o Client ID e Client Secret do Google Cloud.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            btnAuthorizeOAuth.Enabled = false;
            lblOAuthStatus.Text = "Aguardando autorização...";
            lblOAuthStatus.ForeColor = System.Drawing.Color.Gray;

            try
            {
                var secrets = new ClientSecrets { ClientId = clientId, ClientSecret = clientSecret };
                var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    secrets,
                    new[] { DriveService.Scope.Drive },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(System.IO.Path.GetTempPath() + "gdrive-oauth", true));

                _oauthRefreshToken = credential.Token.RefreshToken;

                lblOAuthStatus.Text = "Autorizado com sucesso!";
                lblOAuthStatus.ForeColor = System.Drawing.Color.Green;
                MessageBox.Show(this, "Autorização concluída! O refresh token foi salvo na configuração.",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblOAuthStatus.Text = "Falha na autorização";
                lblOAuthStatus.ForeColor = System.Drawing.Color.Red;
                MessageBox.Show(this, $"Erro ao autorizar:\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnAuthorizeOAuth.Enabled = true;
            }
        }

        private void BtnBrowseFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    txtUploadFilePath.Text = dlg.FileName;
            }
        }

        private void TxtOAuthClientSecret_TextChanged(object sender, EventArgs e)
        {
            _oauthSecretModified = true;
        }

        private void ChkBackupMode_CheckedChanged(object sender, EventArgs e)
        {
            UpdateBackupModeVisibility();
        }

        private static string ParseFolderIdFromLink(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            input = input.Trim();

            var driveMatch = System.Text.RegularExpressions.Regex.Match(input,
                @"drive\.google\.com/drive/(?:u/\d+/)?folders/([^/?&#]+)");
            if (driveMatch.Success)
                return driveMatch.Groups[1].Value;

            var megaMatch = System.Text.RegularExpressions.Regex.Match(input,
                @"mega(?:\.co)?\.nz/folder/([^#/?&]+)");
            if (megaMatch.Success)
                return megaMatch.Groups[1].Value;

            return input;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveConfig(_scheduleCancelled ? "" : dtpSchedule.Value.ToString("yyyy-MM-dd HH:mm"));
        }
        private void SaveConfig(string scheduledTime)
        {
            bool isBackupMode = chkBackupMode.Checked;
            string uploadMode = isBackupMode ? "Backup" : "File";

            if (!isBackupMode && string.IsNullOrWhiteSpace(txtUploadFilePath.Text))
            {
                MessageBox.Show(this, "Selecione um arquivo para upload.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (isBackupMode)
            {
                if (string.IsNullOrWhiteSpace(txtSqlServer.Text))
                {
                    MessageBox.Show(this, "Informe o servidor SQL.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSqlDatabase.Text))
                {
                    MessageBox.Show(this, "Informe o banco de dados.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!chkWindowsAuth.Checked && string.IsNullOrWhiteSpace(txtSqlUsername.Text))
                {
                    MessageBox.Show(this, "Informe o usuário SQL ou marque Autenticação Windows.",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtBackupFolder.Text))
                {
                    MessageBox.Show(this, "Informe a pasta de backup.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string authMethod = rbAuthMega.Checked ? "Mega" : rbAuthOAuth.Checked ? "OAuth" : "ServiceAccount";

            if (authMethod == "ServiceAccount" && string.IsNullOrWhiteSpace(_selectedJsonContent))
            {
                MessageBox.Show(this, "Selecione o arquivo JSON da Service Account.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (authMethod == "OAuth" && string.IsNullOrWhiteSpace(_oauthRefreshToken))
            {
                MessageBox.Show(this, "Clique em \"Autorizar\" para autenticar no Google primeiro.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (authMethod == "Mega")
            {
                if (string.IsNullOrWhiteSpace(txtMegaEmail.Text))
                {
                    MessageBox.Show(this, "Informe o e-mail da conta Mega.",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtMegaPassword.Text))
                {
                    MessageBox.Show(this, "Informe a senha da conta Mega.",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string folderId = ParseFolderIdFromLink(txtFolderLink.Text);

            if (string.IsNullOrWhiteSpace(folderId))
            {
                MessageBox.Show(this, "Informe o link ou ID da pasta de destino.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string encryptPassword = _existingPassword;

            var config = new BackupConfig
            {
                MachineId = _config.MachineId,
                UploadMode = uploadMode,
                UploadFilePath = isBackupMode ? "" : txtUploadFilePath.Text.Trim(),
                SqlServer = txtSqlServer.Text.Trim(),
                SqlDatabase = txtSqlDatabase.Text.Trim(),
                SqlUsername = txtSqlUsername.Text.Trim(),
                SqlPassword = txtSqlPassword.Text,
                UseWindowsAuth = chkWindowsAuth.Checked,
                BackupFolder = txtBackupFolder.Text.Trim(),
                ServiceAccountJsonPath = _jsonFileModified ? txtServiceAccountPath.Text : _config.ServiceAccountJsonPath,
                ServiceAccountJson = _jsonFileModified ? _selectedJsonContent : _config.ServiceAccountJson,
                GoogleDriveFolderId = authMethod != "Mega" ? folderId : _config.GoogleDriveFolderId,
                ScheduledTime = scheduledTime,
                StableSeconds = _config.StableSeconds,
                PollIntervalMs = _config.PollIntervalMs,
                AuthMethod = authMethod,
                OAuthClientId = txtOAuthClientId.Text.Trim(),
                OAuthClientSecret = _oauthSecretModified ? txtOAuthClientSecret.Text : _config.OAuthClientSecret,
                OAuthRefreshToken = _oauthRefreshToken,
                MegaEmail = txtMegaEmail.Text.Trim(),
                MegaPassword = txtMegaPassword.Text,
                MegaFolder = txtMegaFolder.Text,
                MegaFolderId = authMethod == "Mega" ? folderId : _config.MegaFolderId,
                BackupFileName = txtBackupFileName.Text.Trim(),
                DeleteAfterUpload = chkDeleteAfterUpload.Checked,
                DeleteOnFailure = chkDeleteOnFailure.Checked
            };

            try
            {
                var json = config.ToJson();
                var encrypted = SecureStorage.Encrypt(json, encryptPassword);
                SavedPassword = encryptPassword;
                SecureStorage.SaveToFile(SecureStorage.GetDefaultSettingsPath(), encrypted);
                LogService.Write("Configurações salvas com sucesso.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao salvar configurações:\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
