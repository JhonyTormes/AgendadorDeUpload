using System;
using System.Data.SqlClient;
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
        public string SavedPassword { get; private set; }

        public ConfigForm(BackupConfig config, string existingPassword)
        {
            _config = config;
            _existingPassword = existingPassword;
            InitializeComponent();
            LoadConfig();
        }

        private void LoadConfig()
        {
            txtSqlServer.Text = _config.SqlServer ?? "";
            txtSqlDatabase.Text = _config.SqlDatabase ?? "";
            txtSqlUsername.Text = _config.SqlUsername ?? "";
            txtSqlPassword.Text = _config.SqlPassword ?? "";
            chkWindowsAuth.Checked = _config.UseWindowsAuth;
            txtBackupFolder.Text = _config.BackupFolder ?? "";

            if (!string.IsNullOrEmpty(_config.ServiceAccountJsonPath))
                txtServiceAccountPath.Text = _config.ServiceAccountJsonPath;
            _selectedJsonContent = _config.ServiceAccountJson;

            txtOAuthClientId.Text = _config.OAuthClientId ?? "";
            txtOAuthClientSecret.Text = _config.OAuthClientSecret ?? "";
            _oauthRefreshToken = _config.OAuthRefreshToken;

            txtMegaEmail.Text = _config.MegaEmail ?? "";
            txtMegaPassword.Text = _config.MegaPassword ?? "";
            txtMegaFolder.Text = _config.MegaFolder ?? "";

            txtDriveFolderId.Text = _config.GoogleDriveFolderId ?? "";

            chkDeleteAfterUpload.Checked = _config.DeleteAfterUpload;
            chkDeleteOnFailure.Checked = _config.DeleteOnFailure;

            if (_config.AuthMethod == "Mega")
            {
                rbAuthMega.Checked = true;
            }
            else if (_config.AuthMethod == "OAuth" && !string.IsNullOrEmpty(_config.OAuthRefreshToken))
            {
                rbAuthOAuth.Checked = true;
                lblOAuthStatus.Text = "Autorizado";
                lblOAuthStatus.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                rbAuthServiceAccount.Checked = true;
            }

            if (DateTime.TryParseExact(_config.ScheduledTime, "yyyy-MM-dd HH:mm",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, out var dt) && dt > DateTime.Now)
                dtpSchedule.Value = dt;
            else
                dtpSchedule.Value = DateTime.Now.AddHours(1);

            bool hasPassword = !string.IsNullOrEmpty(_existingPassword);
            lblMasterPassword.Visible = !hasPassword;
            txtMasterPassword.Visible = !hasPassword;
            lblConfirmPassword.Visible = !hasPassword;
            txtConfirmPassword.Visible = !hasPassword;

            txtSqlUsername.Enabled = !_config.UseWindowsAuth;
            txtSqlPassword.Enabled = !_config.UseWindowsAuth;

            UpdateAuthVisibility();
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

            bool showDriveId = isSA || isOAuth;
            lblDriveFolderId.Visible = showDriveId;
            txtDriveFolderId.Visible = showDriveId;
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

        private void BtnSelectJson_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog { Filter = "Arquivo JSON|*.json" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtServiceAccountPath.Text = dlg.FileName;
                    _selectedJsonContent = System.IO.File.ReadAllText(dlg.FileName);
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
            try
            {
                INode[] nodes = await Task.Run(() =>
                {
                    var client = new MegaApiClient();
                    client.Login(email, password);
                    var result = client.GetNodes().ToArray();
                    client.Logout();
                    return result;
                });

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
                MessageBox.Show(this, $"Erro ao conectar no Mega:\n{ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSelectMegaFolder.Enabled = true;
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
                var credential = await Task.Run(() =>
                    GoogleWebAuthorizationBroker.AuthorizeAsync(
                        secrets,
                        new[] { DriveService.Scope.Drive },
                        "user",
                        CancellationToken.None,
                        new FileDataStore(System.IO.Path.GetTempPath() + "gdrive-oauth", true))
                );

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

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSqlServer.Text))
            {
                MessageBox.Show(this, "Informe o servidor SQL.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtSqlDatabase.Text))
            {
                MessageBox.Show(this, "Informe o banco de dados.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show(this, "Informe a pasta de backup.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

            if ((authMethod == "ServiceAccount" || authMethod == "OAuth") && string.IsNullOrWhiteSpace(txtDriveFolderId.Text))
            {
                MessageBox.Show(this, "Informe o ID da pasta do Google Drive.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dtpSchedule.Value <= DateTime.Now)
            {
                MessageBox.Show(this, "A data/hora agendada deve ser no futuro.",
                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string encryptPassword;
            if (string.IsNullOrEmpty(_existingPassword))
            {
                var newPassword = txtMasterPassword.Text;
                var confirmPassword = txtConfirmPassword.Text;

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show(this, "Defina uma senha mestra para proteger as configurações.",
                        "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show(this, "As senhas não conferem.", "Validação",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                encryptPassword = newPassword;
            }
            else
            {
                encryptPassword = _existingPassword;
            }

            var config = new BackupConfig
            {
                SqlServer = txtSqlServer.Text.Trim(),
                SqlDatabase = txtSqlDatabase.Text.Trim(),
                SqlUsername = txtSqlUsername.Text.Trim(),
                SqlPassword = txtSqlPassword.Text,
                UseWindowsAuth = chkWindowsAuth.Checked,
                BackupFolder = txtBackupFolder.Text.Trim(),
                ServiceAccountJsonPath = txtServiceAccountPath.Text,
                ServiceAccountJson = _selectedJsonContent,
                GoogleDriveFolderId = txtDriveFolderId.Text.Trim(),
                ScheduledTime = dtpSchedule.Value.ToString("yyyy-MM-dd HH:mm"),
                StableSeconds = _config.StableSeconds,
                PollIntervalMs = _config.PollIntervalMs,
                AuthMethod = authMethod,
                OAuthClientId = txtOAuthClientId.Text.Trim(),
                OAuthClientSecret = txtOAuthClientSecret.Text.Trim(),
                OAuthRefreshToken = _oauthRefreshToken,
                MegaEmail = txtMegaEmail.Text.Trim(),
                MegaPassword = txtMegaPassword.Text,
                MegaFolder = txtMegaFolder.Text,
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
