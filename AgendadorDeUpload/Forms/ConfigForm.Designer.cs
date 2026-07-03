namespace AgendadorDeUpload.Forms
{
    partial class ConfigForm
    {
        private System.Windows.Forms.Label lblSqlServer;
        private System.Windows.Forms.TextBox txtSqlServer;
        private System.Windows.Forms.Label lblSqlDatabase;
        private System.Windows.Forms.TextBox txtSqlDatabase;
        private System.Windows.Forms.CheckBox chkWindowsAuth;
        private System.Windows.Forms.Label lblSqlUsername;
        private System.Windows.Forms.TextBox txtSqlUsername;
        private System.Windows.Forms.Label lblSqlPassword;
        private System.Windows.Forms.TextBox txtSqlPassword;
        private System.Windows.Forms.Button btnTestSqlConnection;
        private System.Windows.Forms.Label lblBackupSection;
        private System.Windows.Forms.Label lblBackupFolder;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.CheckBox chkDeleteAfterUpload;
        private System.Windows.Forms.CheckBox chkDeleteOnFailure;
        private System.Windows.Forms.Label lblGDriveSection;
        private System.Windows.Forms.Label lblServiceAccount;
        private System.Windows.Forms.TextBox txtServiceAccountPath;
        private System.Windows.Forms.Button btnSelectJson;
        private System.Windows.Forms.Label lblDriveFolderId;
        private System.Windows.Forms.TextBox txtDriveFolderId;
        private System.Windows.Forms.Label lblScheduleSection;
        private System.Windows.Forms.Label lblScheduleTime;
        private System.Windows.Forms.DateTimePicker dtpSchedule;
        private System.Windows.Forms.Label lblSecuritySection;
        private System.Windows.Forms.Label lblMasterPassword;
        private System.Windows.Forms.TextBox txtMasterPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private System.Windows.Forms.RadioButton rbAuthMega;
        private System.Windows.Forms.Label lblMegaEmail;
        private System.Windows.Forms.TextBox txtMegaEmail;
        private System.Windows.Forms.Label lblMegaPassword;
        private System.Windows.Forms.TextBox txtMegaPassword;
        private System.Windows.Forms.Label lblMegaFolder;
        private System.Windows.Forms.TextBox txtMegaFolder;
        private System.Windows.Forms.Button btnSelectMegaFolder;
        private System.Windows.Forms.RadioButton rbAuthServiceAccount;
        private System.Windows.Forms.RadioButton rbAuthOAuth;
        private System.Windows.Forms.Label lblOAuthClientId;
        private System.Windows.Forms.TextBox txtOAuthClientId;
        private System.Windows.Forms.Label lblOAuthClientSecret;
        private System.Windows.Forms.TextBox txtOAuthClientSecret;
        private System.Windows.Forms.Button btnAuthorizeOAuth;
        private System.Windows.Forms.Label lblOAuthStatus;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblSqlServer = new System.Windows.Forms.Label();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.lblSqlDatabase = new System.Windows.Forms.Label();
            this.txtSqlDatabase = new System.Windows.Forms.TextBox();
            this.chkWindowsAuth = new System.Windows.Forms.CheckBox();
            this.lblSqlUsername = new System.Windows.Forms.Label();
            this.txtSqlUsername = new System.Windows.Forms.TextBox();
            this.lblSqlPassword = new System.Windows.Forms.Label();
            this.txtSqlPassword = new System.Windows.Forms.TextBox();
            this.lblBackupSection = new System.Windows.Forms.Label();
            this.lblBackupFolder = new System.Windows.Forms.Label();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.lblGDriveSection = new System.Windows.Forms.Label();
            this.lblServiceAccount = new System.Windows.Forms.Label();
            this.txtServiceAccountPath = new System.Windows.Forms.TextBox();
            this.btnSelectJson = new System.Windows.Forms.Button();
            this.lblDriveFolderId = new System.Windows.Forms.Label();
            this.txtDriveFolderId = new System.Windows.Forms.TextBox();
            this.lblScheduleSection = new System.Windows.Forms.Label();
            this.lblScheduleTime = new System.Windows.Forms.Label();
            this.dtpSchedule = new System.Windows.Forms.DateTimePicker();
            this.lblSecuritySection = new System.Windows.Forms.Label();
            this.lblMasterPassword = new System.Windows.Forms.Label();
            this.txtMasterPassword = new System.Windows.Forms.TextBox();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbAuthServiceAccount = new System.Windows.Forms.RadioButton();
            this.rbAuthOAuth = new System.Windows.Forms.RadioButton();
            this.lblOAuthClientId = new System.Windows.Forms.Label();
            this.txtOAuthClientId = new System.Windows.Forms.TextBox();
            this.lblOAuthClientSecret = new System.Windows.Forms.Label();
            this.txtOAuthClientSecret = new System.Windows.Forms.TextBox();
            this.btnAuthorizeOAuth = new System.Windows.Forms.Button();
            this.lblOAuthStatus = new System.Windows.Forms.Label();
            this.rbAuthMega = new System.Windows.Forms.RadioButton();
            this.lblMegaEmail = new System.Windows.Forms.Label();
            this.txtMegaEmail = new System.Windows.Forms.TextBox();
            this.lblMegaPassword = new System.Windows.Forms.Label();
            this.txtMegaPassword = new System.Windows.Forms.TextBox();

            this.SuspendLayout();

            // lblSqlServer
            this.lblSqlServer.Text = "Servidor SQL:";
            this.lblSqlServer.Location = new System.Drawing.Point(10, 14);
            this.lblSqlServer.Size = new System.Drawing.Size(140, 22);
            this.lblSqlServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtSqlServer
            this.txtSqlServer.Location = new System.Drawing.Point(155, 14);
            this.txtSqlServer.Size = new System.Drawing.Size(330, 22);

            // lblSqlDatabase
            this.lblSqlDatabase.Text = "Banco de Dados:";
            this.lblSqlDatabase.Location = new System.Drawing.Point(10, 42);
            this.lblSqlDatabase.Size = new System.Drawing.Size(140, 22);
            this.lblSqlDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtSqlDatabase
            this.txtSqlDatabase.Location = new System.Drawing.Point(155, 42);
            this.txtSqlDatabase.Size = new System.Drawing.Size(330, 22);

            // chkWindowsAuth
            this.chkWindowsAuth.Text = "Usar Autenticação Windows";
            this.chkWindowsAuth.Location = new System.Drawing.Point(155, 70);
            this.chkWindowsAuth.Size = new System.Drawing.Size(300, 22);
            this.chkWindowsAuth.CheckedChanged += this.ChkWindowsAuth_CheckedChanged;

            // lblSqlUsername
            this.lblSqlUsername.Text = "Usuário SQL:";
            this.lblSqlUsername.Location = new System.Drawing.Point(10, 98);
            this.lblSqlUsername.Size = new System.Drawing.Size(140, 22);
            this.lblSqlUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtSqlUsername
            this.txtSqlUsername.Location = new System.Drawing.Point(155, 98);
            this.txtSqlUsername.Size = new System.Drawing.Size(330, 22);

            // lblSqlPassword
            this.lblSqlPassword.Text = "Senha SQL:";
            this.lblSqlPassword.Location = new System.Drawing.Point(10, 126);
            this.lblSqlPassword.Size = new System.Drawing.Size(140, 22);
            this.lblSqlPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtSqlPassword
            this.txtSqlPassword.Location = new System.Drawing.Point(155, 126);
            this.txtSqlPassword.Size = new System.Drawing.Size(330, 22);
            this.txtSqlPassword.PasswordChar = '*';

            // btnTestSqlConnection
            this.btnTestSqlConnection = new System.Windows.Forms.Button();
            this.btnTestSqlConnection.Text = "Testar Conexão SQL";
            this.btnTestSqlConnection.Location = new System.Drawing.Point(155, 150);
            this.btnTestSqlConnection.Size = new System.Drawing.Size(120, 22);
            this.btnTestSqlConnection.Click += this.BtnTestSqlConnection_Click;

            // lblBackupSection
            this.lblBackupSection.Text = "Backup";
            this.lblBackupSection.Location = new System.Drawing.Point(10, 182);
            this.lblBackupSection.Size = new System.Drawing.Size(480, 20);
            this.lblBackupSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // lblBackupFolder
            this.lblBackupFolder.Text = "Pasta de Backup:";
            this.lblBackupFolder.Location = new System.Drawing.Point(10, 206);
            this.lblBackupFolder.Size = new System.Drawing.Size(140, 22);
            this.lblBackupFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtBackupFolder
            this.txtBackupFolder.Location = new System.Drawing.Point(155, 206);
            this.txtBackupFolder.Size = new System.Drawing.Size(300, 22);

            // btnBrowseFolder
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.Location = new System.Drawing.Point(457, 206);
            this.btnBrowseFolder.Size = new System.Drawing.Size(28, 22);
            this.btnBrowseFolder.Click += this.BtnBrowseFolder_Click;

            // chkDeleteAfterUpload
            this.chkDeleteAfterUpload = new System.Windows.Forms.CheckBox();
            this.chkDeleteAfterUpload.Text = "Deletar backup após upload";
            this.chkDeleteAfterUpload.Location = new System.Drawing.Point(155, 235);
            this.chkDeleteAfterUpload.Size = new System.Drawing.Size(250, 22);

            // chkDeleteOnFailure
            this.chkDeleteOnFailure = new System.Windows.Forms.CheckBox();
            this.chkDeleteOnFailure.Text = "Deletar backup em caso de falha no Upload";
            this.chkDeleteOnFailure.Location = new System.Drawing.Point(155, 258);
            this.chkDeleteOnFailure.Size = new System.Drawing.Size(330, 22);

            // lblGDriveSection
            this.lblGDriveSection.Text = "Destino do Upload";
            this.lblGDriveSection.Location = new System.Drawing.Point(10, 292);
            this.lblGDriveSection.Size = new System.Drawing.Size(480, 20);
            this.lblGDriveSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // rbAuthServiceAccount
            this.rbAuthServiceAccount.Text = "Drive (Conta de Serviço)";
            this.rbAuthServiceAccount.Location = new System.Drawing.Point(155, 315);
            this.rbAuthServiceAccount.Size = new System.Drawing.Size(130, 22);
            this.rbAuthServiceAccount.CheckedChanged += this.RbAuthMethod_CheckedChanged;

            // rbAuthOAuth
            this.rbAuthOAuth.Text = "Drive (OAuth)";
            this.rbAuthOAuth.Location = new System.Drawing.Point(290, 315);
            this.rbAuthOAuth.Size = new System.Drawing.Size(100, 22);
            this.rbAuthOAuth.CheckedChanged += this.RbAuthMethod_CheckedChanged;

            // rbAuthMega
            this.rbAuthMega.Text = "Mega";
            this.rbAuthMega.Location = new System.Drawing.Point(395, 315);
            this.rbAuthMega.Size = new System.Drawing.Size(70, 22);
            this.rbAuthMega.CheckedChanged += this.RbAuthMethod_CheckedChanged;

            // lblServiceAccount
            this.lblServiceAccount.Text = "Arquivo JSON:";
            this.lblServiceAccount.Location = new System.Drawing.Point(10, 342);
            this.lblServiceAccount.Size = new System.Drawing.Size(140, 22);
            this.lblServiceAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtServiceAccountPath
            this.txtServiceAccountPath.Location = new System.Drawing.Point(155, 342);
            this.txtServiceAccountPath.Size = new System.Drawing.Size(300, 22);
            this.txtServiceAccountPath.ReadOnly = true;

            // btnSelectJson
            this.btnSelectJson.Text = "...";
            this.btnSelectJson.Location = new System.Drawing.Point(457, 342);
            this.btnSelectJson.Size = new System.Drawing.Size(28, 22);
            this.btnSelectJson.Click += this.BtnSelectJson_Click;

            // lblOAuthClientId
            this.lblOAuthClientId.Text = "Client ID:";
            this.lblOAuthClientId.Location = new System.Drawing.Point(10, 342);
            this.lblOAuthClientId.Size = new System.Drawing.Size(140, 22);
            this.lblOAuthClientId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtOAuthClientId
            this.txtOAuthClientId.Location = new System.Drawing.Point(155, 342);
            this.txtOAuthClientId.Size = new System.Drawing.Size(330, 22);

            // lblOAuthClientSecret
            this.lblOAuthClientSecret.Text = "Client Secret:";
            this.lblOAuthClientSecret.Location = new System.Drawing.Point(10, 370);
            this.lblOAuthClientSecret.Size = new System.Drawing.Size(140, 22);
            this.lblOAuthClientSecret.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtOAuthClientSecret
            this.txtOAuthClientSecret.Location = new System.Drawing.Point(155, 370);
            this.txtOAuthClientSecret.Size = new System.Drawing.Size(250, 22);
            this.txtOAuthClientSecret.PasswordChar = '*';

            // btnAuthorizeOAuth
            this.btnAuthorizeOAuth.Text = "Autorizar";
            this.btnAuthorizeOAuth.Location = new System.Drawing.Point(415, 370);
            this.btnAuthorizeOAuth.Size = new System.Drawing.Size(70, 22);
            this.btnAuthorizeOAuth.Click += this.BtnAuthorizeOAuth_Click;

            // lblOAuthStatus
            this.lblOAuthStatus.Text = "Não autorizado";
            this.lblOAuthStatus.Location = new System.Drawing.Point(155, 398);
            this.lblOAuthStatus.Size = new System.Drawing.Size(300, 22);
            this.lblOAuthStatus.ForeColor = System.Drawing.Color.Gray;

            // lblMegaEmail
            this.lblMegaEmail.Text = "E-mail Mega:";
            this.lblMegaEmail.Location = new System.Drawing.Point(10, 342);
            this.lblMegaEmail.Size = new System.Drawing.Size(140, 22);
            this.lblMegaEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtMegaEmail
            this.txtMegaEmail.Location = new System.Drawing.Point(155, 342);
            this.txtMegaEmail.Size = new System.Drawing.Size(330, 22);

            // lblMegaPassword
            this.lblMegaPassword.Text = "Senha Mega:";
            this.lblMegaPassword.Location = new System.Drawing.Point(10, 370);
            this.lblMegaPassword.Size = new System.Drawing.Size(140, 22);
            this.lblMegaPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtMegaPassword
            this.txtMegaPassword.Location = new System.Drawing.Point(155, 370);
            this.txtMegaPassword.Size = new System.Drawing.Size(250, 22);
            this.txtMegaPassword.PasswordChar = '*';

            // lblMegaFolder
            this.lblMegaFolder = new System.Windows.Forms.Label();
            this.lblMegaFolder.Text = "Pasta Mega:";
            this.lblMegaFolder.Location = new System.Drawing.Point(10, 398);
            this.lblMegaFolder.Size = new System.Drawing.Size(140, 22);
            this.lblMegaFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtMegaFolder
            this.txtMegaFolder = new System.Windows.Forms.TextBox();
            this.txtMegaFolder.Location = new System.Drawing.Point(155, 398);
            this.txtMegaFolder.Size = new System.Drawing.Size(300, 22);
            this.txtMegaFolder.ReadOnly = true;

            // btnSelectMegaFolder
            this.btnSelectMegaFolder = new System.Windows.Forms.Button();
            this.btnSelectMegaFolder.Text = "Selecionar...";
            this.btnSelectMegaFolder.Location = new System.Drawing.Point(457, 398);
            this.btnSelectMegaFolder.Size = new System.Drawing.Size(50, 22);
            this.btnSelectMegaFolder.Click += this.BtnSelectMegaFolder_Click;

            // lblDriveFolderId
            this.lblDriveFolderId.Text = "Pasta ID:";
            this.lblDriveFolderId.Location = new System.Drawing.Point(10, 430);
            this.lblDriveFolderId.Size = new System.Drawing.Size(140, 22);
            this.lblDriveFolderId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtDriveFolderId
            this.txtDriveFolderId.Location = new System.Drawing.Point(155, 430);
            this.txtDriveFolderId.Size = new System.Drawing.Size(330, 22);

            // lblScheduleSection
            this.lblScheduleSection.Text = "Agendamento";
            this.lblScheduleSection.Location = new System.Drawing.Point(10, 466);
            this.lblScheduleSection.Size = new System.Drawing.Size(480, 20);
            this.lblScheduleSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // lblScheduleTime
            this.lblScheduleTime.Text = "Data e Hora:";
            this.lblScheduleTime.Location = new System.Drawing.Point(10, 490);
            this.lblScheduleTime.Size = new System.Drawing.Size(140, 22);
            this.lblScheduleTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // dtpSchedule
            this.dtpSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSchedule.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpSchedule.Location = new System.Drawing.Point(155, 490);
            this.dtpSchedule.Size = new System.Drawing.Size(140, 22);

            // lblSecuritySection
            this.lblSecuritySection.Text = "Segurança";
            this.lblSecuritySection.Location = new System.Drawing.Point(10, 526);
            this.lblSecuritySection.Size = new System.Drawing.Size(480, 20);
            this.lblSecuritySection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // lblMasterPassword
            this.lblMasterPassword.Text = "Nova Senha Mestra:";
            this.lblMasterPassword.Location = new System.Drawing.Point(10, 550);
            this.lblMasterPassword.Size = new System.Drawing.Size(140, 22);
            this.lblMasterPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtMasterPassword
            this.txtMasterPassword.Location = new System.Drawing.Point(155, 550);
            this.txtMasterPassword.Size = new System.Drawing.Size(250, 22);
            this.txtMasterPassword.PasswordChar = '*';

            // lblConfirmPassword
            this.lblConfirmPassword.Text = "Confirmar Senha:";
            this.lblConfirmPassword.Location = new System.Drawing.Point(10, 578);
            this.lblConfirmPassword.Size = new System.Drawing.Size(140, 22);
            this.lblConfirmPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            // txtConfirmPassword
            this.txtConfirmPassword.Location = new System.Drawing.Point(155, 578);
            this.txtConfirmPassword.Size = new System.Drawing.Size(250, 22);
            this.txtConfirmPassword.PasswordChar = '*';

            // btnSave
            this.btnSave.Text = "Salvar";
            this.btnSave.Location = new System.Drawing.Point(310, 615);
            this.btnSave.Size = new System.Drawing.Size(90, 28);

            // btnCancel
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Location = new System.Drawing.Point(406, 615);
            this.btnCancel.Size = new System.Drawing.Size(90, 28);
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;

            // ConfigForm
            this.ClientSize = new System.Drawing.Size(520, 660);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações - Agendador de Upload";

            this.Controls.Add(this.lblSqlServer);
            this.Controls.Add(this.txtSqlServer);
            this.Controls.Add(this.lblSqlDatabase);
            this.Controls.Add(this.txtSqlDatabase);
            this.Controls.Add(this.chkWindowsAuth);
            this.Controls.Add(this.lblSqlUsername);
            this.Controls.Add(this.txtSqlUsername);
            this.Controls.Add(this.lblSqlPassword);
            this.Controls.Add(this.txtSqlPassword);
            this.Controls.Add(this.btnTestSqlConnection);
            this.Controls.Add(this.lblBackupSection);
            this.Controls.Add(this.lblBackupFolder);
            this.Controls.Add(this.txtBackupFolder);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.chkDeleteAfterUpload);
            this.Controls.Add(this.chkDeleteOnFailure);
            this.Controls.Add(this.lblGDriveSection);
            this.Controls.Add(this.rbAuthServiceAccount);
            this.Controls.Add(this.rbAuthOAuth);
            this.Controls.Add(this.lblServiceAccount);
            this.Controls.Add(this.txtServiceAccountPath);
            this.Controls.Add(this.btnSelectJson);
            this.Controls.Add(this.lblOAuthClientId);
            this.Controls.Add(this.txtOAuthClientId);
            this.Controls.Add(this.lblOAuthClientSecret);
            this.Controls.Add(this.txtOAuthClientSecret);
            this.Controls.Add(this.btnAuthorizeOAuth);
            this.Controls.Add(this.lblOAuthStatus);
            this.Controls.Add(this.rbAuthMega);
            this.Controls.Add(this.lblMegaEmail);
            this.Controls.Add(this.txtMegaEmail);
            this.Controls.Add(this.lblMegaPassword);
            this.Controls.Add(this.txtMegaPassword);
            this.Controls.Add(this.lblMegaFolder);
            this.Controls.Add(this.txtMegaFolder);
            this.Controls.Add(this.btnSelectMegaFolder);
            this.Controls.Add(this.lblDriveFolderId);
            this.Controls.Add(this.txtDriveFolderId);
            this.Controls.Add(this.lblScheduleSection);
            this.Controls.Add(this.lblScheduleTime);
            this.Controls.Add(this.dtpSchedule);
            this.Controls.Add(this.lblSecuritySection);
            this.Controls.Add(this.lblMasterPassword);
            this.Controls.Add(this.txtMasterPassword);
            this.Controls.Add(this.lblConfirmPassword);
            this.Controls.Add(this.txtConfirmPassword);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
