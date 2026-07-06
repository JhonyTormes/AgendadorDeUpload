namespace AgendadorDeUpload.Forms
{
    partial class ConfigForm
    {
        private System.Windows.Forms.Label lblFileSection;
        private System.Windows.Forms.Label lblUploadFile;
        private System.Windows.Forms.TextBox txtUploadFilePath;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Label lblDestinoSection;
        private System.Windows.Forms.Label lblServiceAccount;
        private System.Windows.Forms.TextBox txtServiceAccountPath;
        private System.Windows.Forms.Button btnSelectJson;
        private System.Windows.Forms.Label lblFolderLink;
        private System.Windows.Forms.TextBox txtFolderLink;
        private System.Windows.Forms.Label lblScheduleSection;
        private System.Windows.Forms.Label lblScheduleTime;
        private System.Windows.Forms.DateTimePicker dtpSchedule;
        private System.Windows.Forms.Button btnScheduleAction;
        private System.Windows.Forms.CheckBox chkBackupMode;
        private System.Windows.Forms.GroupBox grpBackupSql;
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
        private System.Windows.Forms.Label lblBackupFolder;
        private System.Windows.Forms.TextBox txtBackupFolder;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.Button btnTestFolderPerm;
        private System.Windows.Forms.CheckBox chkDeleteAfterUpload;
        private System.Windows.Forms.CheckBox chkDeleteOnFailure;
        private System.Windows.Forms.Label lblBackupFileName;
        private System.Windows.Forms.TextBox txtBackupFileName;
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
        private System.Windows.Forms.Button btnTestMegaLogin;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFileSection = new System.Windows.Forms.Label();
            this.lblUploadFile = new System.Windows.Forms.Label();
            this.txtUploadFilePath = new System.Windows.Forms.TextBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.lblDestinoSection = new System.Windows.Forms.Label();
            this.rbAuthMega = new System.Windows.Forms.RadioButton();
            this.rbAuthServiceAccount = new System.Windows.Forms.RadioButton();
            this.rbAuthOAuth = new System.Windows.Forms.RadioButton();
            this.lblServiceAccount = new System.Windows.Forms.Label();
            this.txtServiceAccountPath = new System.Windows.Forms.TextBox();
            this.btnSelectJson = new System.Windows.Forms.Button();
            this.lblOAuthClientId = new System.Windows.Forms.Label();
            this.txtOAuthClientId = new System.Windows.Forms.TextBox();
            this.lblOAuthClientSecret = new System.Windows.Forms.Label();
            this.txtOAuthClientSecret = new System.Windows.Forms.TextBox();
            this.btnAuthorizeOAuth = new System.Windows.Forms.Button();
            this.lblOAuthStatus = new System.Windows.Forms.Label();
            this.lblMegaEmail = new System.Windows.Forms.Label();
            this.txtMegaEmail = new System.Windows.Forms.TextBox();
            this.lblMegaPassword = new System.Windows.Forms.Label();
            this.txtMegaPassword = new System.Windows.Forms.TextBox();
            this.btnTestMegaLogin = new System.Windows.Forms.Button();
            this.lblMegaFolder = new System.Windows.Forms.Label();
            this.txtMegaFolder = new System.Windows.Forms.TextBox();
            this.btnSelectMegaFolder = new System.Windows.Forms.Button();
            this.lblFolderLink = new System.Windows.Forms.Label();
            this.txtFolderLink = new System.Windows.Forms.TextBox();
            this.lblScheduleSection = new System.Windows.Forms.Label();
            this.lblScheduleTime = new System.Windows.Forms.Label();
            this.dtpSchedule = new System.Windows.Forms.DateTimePicker();
            this.btnScheduleAction = new System.Windows.Forms.Button();
            this.chkBackupMode = new System.Windows.Forms.CheckBox();
            this.grpBackupSql = new System.Windows.Forms.GroupBox();
            this.lblSqlServer = new System.Windows.Forms.Label();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.lblSqlDatabase = new System.Windows.Forms.Label();
            this.txtSqlDatabase = new System.Windows.Forms.TextBox();
            this.chkWindowsAuth = new System.Windows.Forms.CheckBox();
            this.lblSqlUsername = new System.Windows.Forms.Label();
            this.txtSqlUsername = new System.Windows.Forms.TextBox();
            this.lblSqlPassword = new System.Windows.Forms.Label();
            this.txtSqlPassword = new System.Windows.Forms.TextBox();
            this.btnTestSqlConnection = new System.Windows.Forms.Button();
            this.lblBackupFolder = new System.Windows.Forms.Label();
            this.txtBackupFolder = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.btnTestFolderPerm = new System.Windows.Forms.Button();
            this.chkDeleteAfterUpload = new System.Windows.Forms.CheckBox();
            this.chkDeleteOnFailure = new System.Windows.Forms.CheckBox();
            this.lblBackupFileName = new System.Windows.Forms.Label();
            this.txtBackupFileName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpBackupSql.SuspendLayout();
            this.SuspendLayout();
            //
            // lblFileSection
            //
            this.lblFileSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFileSection.Location = new System.Drawing.Point(10, 14);
            this.lblFileSection.Name = "lblFileSection";
            this.lblFileSection.Size = new System.Drawing.Size(500, 20);
            this.lblFileSection.TabIndex = 0;
            this.lblFileSection.Text = "Arquivo para Upload";
            //
            // lblUploadFile
            //
            this.lblUploadFile.Location = new System.Drawing.Point(10, 40);
            this.lblUploadFile.Name = "lblUploadFile";
            this.lblUploadFile.Size = new System.Drawing.Size(140, 22);
            this.lblUploadFile.TabIndex = 1;
            this.lblUploadFile.Text = "Arquivo:";
            this.lblUploadFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtUploadFilePath
            //
            this.txtUploadFilePath.Location = new System.Drawing.Point(155, 40);
            this.txtUploadFilePath.Name = "txtUploadFilePath";
            this.txtUploadFilePath.ReadOnly = true;
            this.txtUploadFilePath.Size = new System.Drawing.Size(280, 20);
            this.txtUploadFilePath.TabIndex = 2;
            //
            // btnBrowseFile
            //
            this.btnBrowseFile.Location = new System.Drawing.Point(440, 39);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(28, 22);
            this.btnBrowseFile.TabIndex = 3;
            this.btnBrowseFile.Text = "...";
            this.btnBrowseFile.Click += new System.EventHandler(this.BtnBrowseFile_Click);
            //
            // lblDestinoSection
            //
            this.lblDestinoSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDestinoSection.Location = new System.Drawing.Point(10, 78);
            this.lblDestinoSection.Name = "lblDestinoSection";
            this.lblDestinoSection.Size = new System.Drawing.Size(500, 20);
            this.lblDestinoSection.TabIndex = 4;
            this.lblDestinoSection.Text = "Destino do Upload";
            //
            // rbAuthMega
            //
            this.rbAuthMega.Location = new System.Drawing.Point(155, 100);
            this.rbAuthMega.Name = "rbAuthMega";
            this.rbAuthMega.Size = new System.Drawing.Size(70, 22);
            this.rbAuthMega.TabIndex = 5;
            this.rbAuthMega.Text = "Mega";
            this.rbAuthMega.CheckedChanged += new System.EventHandler(this.RbAuthMethod_CheckedChanged);
            //
            // rbAuthServiceAccount
            //
            this.rbAuthServiceAccount.Location = new System.Drawing.Point(230, 100);
            this.rbAuthServiceAccount.Name = "rbAuthServiceAccount";
            this.rbAuthServiceAccount.Size = new System.Drawing.Size(158, 22);
            this.rbAuthServiceAccount.TabIndex = 6;
            this.rbAuthServiceAccount.Text = "Drive (Conta de Serviço)";
            this.rbAuthServiceAccount.CheckedChanged += new System.EventHandler(this.RbAuthMethod_CheckedChanged);
            //
            // rbAuthOAuth
            //
            this.rbAuthOAuth.Location = new System.Drawing.Point(391, 100);
            this.rbAuthOAuth.Name = "rbAuthOAuth";
            this.rbAuthOAuth.Size = new System.Drawing.Size(100, 22);
            this.rbAuthOAuth.TabIndex = 7;
            this.rbAuthOAuth.Text = "Drive (OAuth)";
            this.rbAuthOAuth.CheckedChanged += new System.EventHandler(this.RbAuthMethod_CheckedChanged);
            //
            // lblServiceAccount
            //
            this.lblServiceAccount.Location = new System.Drawing.Point(10, 128);
            this.lblServiceAccount.Name = "lblServiceAccount";
            this.lblServiceAccount.Size = new System.Drawing.Size(140, 22);
            this.lblServiceAccount.TabIndex = 8;
            this.lblServiceAccount.Text = "Arquivo JSON:";
            this.lblServiceAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtServiceAccountPath
            //
            this.txtServiceAccountPath.Location = new System.Drawing.Point(155, 128);
            this.txtServiceAccountPath.Name = "txtServiceAccountPath";
            this.txtServiceAccountPath.ReadOnly = true;
            this.txtServiceAccountPath.Size = new System.Drawing.Size(280, 20);
            this.txtServiceAccountPath.TabIndex = 9;
            //
            // btnSelectJson
            //
            this.btnSelectJson.Location = new System.Drawing.Point(440, 127);
            this.btnSelectJson.Name = "btnSelectJson";
            this.btnSelectJson.Size = new System.Drawing.Size(28, 22);
            this.btnSelectJson.TabIndex = 10;
            this.btnSelectJson.Text = "...";
            this.btnSelectJson.Click += new System.EventHandler(this.BtnSelectJson_Click);
            //
            // lblOAuthClientId
            //
            this.lblOAuthClientId.Location = new System.Drawing.Point(10, 128);
            this.lblOAuthClientId.Name = "lblOAuthClientId";
            this.lblOAuthClientId.Size = new System.Drawing.Size(140, 22);
            this.lblOAuthClientId.TabIndex = 11;
            this.lblOAuthClientId.Text = "Client ID:";
            this.lblOAuthClientId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtOAuthClientId
            //
            this.txtOAuthClientId.Location = new System.Drawing.Point(155, 128);
            this.txtOAuthClientId.Name = "txtOAuthClientId";
            this.txtOAuthClientId.Size = new System.Drawing.Size(330, 20);
            this.txtOAuthClientId.TabIndex = 12;
            //
            // lblOAuthClientSecret
            //
            this.lblOAuthClientSecret.Location = new System.Drawing.Point(10, 156);
            this.lblOAuthClientSecret.Name = "lblOAuthClientSecret";
            this.lblOAuthClientSecret.Size = new System.Drawing.Size(140, 22);
            this.lblOAuthClientSecret.TabIndex = 13;
            this.lblOAuthClientSecret.Text = "Client Secret:";
            this.lblOAuthClientSecret.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtOAuthClientSecret
            //
            this.txtOAuthClientSecret.Location = new System.Drawing.Point(155, 156);
            this.txtOAuthClientSecret.Name = "txtOAuthClientSecret";
            this.txtOAuthClientSecret.Size = new System.Drawing.Size(330, 20);
            this.txtOAuthClientSecret.TabIndex = 14;
            //
            // btnAuthorizeOAuth
            //
            this.btnAuthorizeOAuth.Location = new System.Drawing.Point(155, 184);
            this.btnAuthorizeOAuth.Name = "btnAuthorizeOAuth";
            this.btnAuthorizeOAuth.Size = new System.Drawing.Size(75, 23);
            this.btnAuthorizeOAuth.TabIndex = 15;
            this.btnAuthorizeOAuth.Text = "Autorizar";
            this.btnAuthorizeOAuth.Click += new System.EventHandler(this.BtnAuthorizeOAuth_Click);
            //
            // lblOAuthStatus
            //
            this.lblOAuthStatus.Location = new System.Drawing.Point(240, 186);
            this.lblOAuthStatus.Name = "lblOAuthStatus";
            this.lblOAuthStatus.Size = new System.Drawing.Size(250, 22);
            this.lblOAuthStatus.TabIndex = 16;
            this.lblOAuthStatus.Text = "Não autorizado";
            this.lblOAuthStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblMegaEmail
            //
            this.lblMegaEmail.Location = new System.Drawing.Point(10, 128);
            this.lblMegaEmail.Name = "lblMegaEmail";
            this.lblMegaEmail.Size = new System.Drawing.Size(140, 22);
            this.lblMegaEmail.TabIndex = 17;
            this.lblMegaEmail.Text = "E-mail Mega:";
            this.lblMegaEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtMegaEmail
            //
            this.txtMegaEmail.Location = new System.Drawing.Point(155, 128);
            this.txtMegaEmail.Name = "txtMegaEmail";
            this.txtMegaEmail.Size = new System.Drawing.Size(330, 20);
            this.txtMegaEmail.TabIndex = 18;
            //
            // lblMegaPassword
            //
            this.lblMegaPassword.Location = new System.Drawing.Point(10, 156);
            this.lblMegaPassword.Name = "lblMegaPassword";
            this.lblMegaPassword.Size = new System.Drawing.Size(140, 22);
            this.lblMegaPassword.TabIndex = 19;
            this.lblMegaPassword.Text = "Senha Mega:";
            this.lblMegaPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtMegaPassword
            //
            this.txtMegaPassword.Location = new System.Drawing.Point(155, 156);
            this.txtMegaPassword.Name = "txtMegaPassword";
            this.txtMegaPassword.PasswordChar = '*';
            this.txtMegaPassword.Size = new System.Drawing.Size(250, 20);
            this.txtMegaPassword.TabIndex = 20;
            //
            // btnTestMegaLogin
            //
            this.btnTestMegaLogin.Location = new System.Drawing.Point(410, 156);
            this.btnTestMegaLogin.Name = "btnTestMegaLogin";
            this.btnTestMegaLogin.Size = new System.Drawing.Size(75, 22);
            this.btnTestMegaLogin.TabIndex = 21;
            this.btnTestMegaLogin.Text = "Testar";
            this.btnTestMegaLogin.Click += new System.EventHandler(this.BtnTestMegaLogin_Click);
            //
            // lblMegaFolder
            //
            this.lblMegaFolder.Location = new System.Drawing.Point(10, 184);
            this.lblMegaFolder.Name = "lblMegaFolder";
            this.lblMegaFolder.Size = new System.Drawing.Size(140, 22);
            this.lblMegaFolder.TabIndex = 22;
            this.lblMegaFolder.Text = "Pasta Mega:";
            this.lblMegaFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtMegaFolder
            //
            this.txtMegaFolder.Location = new System.Drawing.Point(155, 184);
            this.txtMegaFolder.Name = "txtMegaFolder";
            this.txtMegaFolder.ReadOnly = true;
            this.txtMegaFolder.Size = new System.Drawing.Size(280, 20);
            this.txtMegaFolder.TabIndex = 23;
            //
            // btnSelectMegaFolder
            //
            this.btnSelectMegaFolder.Location = new System.Drawing.Point(440, 183);
            this.btnSelectMegaFolder.Name = "btnSelectMegaFolder";
            this.btnSelectMegaFolder.Size = new System.Drawing.Size(28, 22);
            this.btnSelectMegaFolder.TabIndex = 24;
            this.btnSelectMegaFolder.Text = "...";
            this.btnSelectMegaFolder.Click += new System.EventHandler(this.BtnSelectMegaFolder_Click);
            //
            // lblFolderLink
            //
            this.lblFolderLink.Location = new System.Drawing.Point(10, 216);
            this.lblFolderLink.Name = "lblFolderLink";
            this.lblFolderLink.Size = new System.Drawing.Size(140, 22);
            this.lblFolderLink.TabIndex = 25;
            this.lblFolderLink.Text = "Link/ID da Pasta:";
            this.lblFolderLink.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtFolderLink
            //
            this.txtFolderLink.Location = new System.Drawing.Point(155, 216);
            this.txtFolderLink.Name = "txtFolderLink";
            this.txtFolderLink.Size = new System.Drawing.Size(330, 20);
            this.txtFolderLink.TabIndex = 26;
            //
            // lblScheduleSection
            //
            this.lblScheduleSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblScheduleSection.Location = new System.Drawing.Point(10, 258);
            this.lblScheduleSection.Name = "lblScheduleSection";
            this.lblScheduleSection.Size = new System.Drawing.Size(500, 20);
            this.lblScheduleSection.TabIndex = 27;
            this.lblScheduleSection.Text = "Agendamento";
            //
            // lblScheduleTime
            //
            this.lblScheduleTime.Location = new System.Drawing.Point(10, 282);
            this.lblScheduleTime.Name = "lblScheduleTime";
            this.lblScheduleTime.Size = new System.Drawing.Size(140, 22);
            this.lblScheduleTime.TabIndex = 28;
            this.lblScheduleTime.Text = "Data e Hora:";
            this.lblScheduleTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // dtpSchedule
            //
            this.dtpSchedule.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSchedule.Location = new System.Drawing.Point(155, 282);
            this.dtpSchedule.Name = "dtpSchedule";
            this.dtpSchedule.Size = new System.Drawing.Size(140, 20);
            this.dtpSchedule.TabIndex = 29;
            //
            // btnScheduleAction
            //
            this.btnScheduleAction.Location = new System.Drawing.Point(300, 280);
            this.btnScheduleAction.Name = "btnScheduleAction";
            this.btnScheduleAction.Size = new System.Drawing.Size(110, 25);
            this.btnScheduleAction.TabIndex = 30;
            this.btnScheduleAction.Text = "Agendar";
            this.btnScheduleAction.Click += new System.EventHandler(this.BtnScheduleAction_Click);
            //
            // chkBackupMode
            //
            this.chkBackupMode.Location = new System.Drawing.Point(10, 320);
            this.chkBackupMode.Name = "chkBackupMode";
            this.chkBackupMode.Size = new System.Drawing.Size(490, 22);
            this.chkBackupMode.TabIndex = 31;
            this.chkBackupMode.Text = "Configurar backup SQL (avançado)";
            this.chkBackupMode.CheckedChanged += new System.EventHandler(this.ChkBackupMode_CheckedChanged);
            //
            // grpBackupSql
            //
            this.grpBackupSql.Controls.Add(this.lblSqlServer);
            this.grpBackupSql.Controls.Add(this.txtSqlServer);
            this.grpBackupSql.Controls.Add(this.lblSqlDatabase);
            this.grpBackupSql.Controls.Add(this.txtSqlDatabase);
            this.grpBackupSql.Controls.Add(this.chkWindowsAuth);
            this.grpBackupSql.Controls.Add(this.lblSqlUsername);
            this.grpBackupSql.Controls.Add(this.txtSqlUsername);
            this.grpBackupSql.Controls.Add(this.lblSqlPassword);
            this.grpBackupSql.Controls.Add(this.txtSqlPassword);
            this.grpBackupSql.Controls.Add(this.btnTestSqlConnection);
            this.grpBackupSql.Controls.Add(this.lblBackupFolder);
            this.grpBackupSql.Controls.Add(this.txtBackupFolder);
            this.grpBackupSql.Controls.Add(this.btnBrowseFolder);
            this.grpBackupSql.Controls.Add(this.btnTestFolderPerm);
            this.grpBackupSql.Controls.Add(this.chkDeleteAfterUpload);
            this.grpBackupSql.Controls.Add(this.chkDeleteOnFailure);
            this.grpBackupSql.Controls.Add(this.lblBackupFileName);
            this.grpBackupSql.Controls.Add(this.txtBackupFileName);
            this.grpBackupSql.Location = new System.Drawing.Point(10, 350);
            this.grpBackupSql.Name = "grpBackupSql";
            this.grpBackupSql.Size = new System.Drawing.Size(500, 300);
            this.grpBackupSql.TabIndex = 32;
            this.grpBackupSql.TabStop = false;
            this.grpBackupSql.Text = "Backup SQL";
            this.grpBackupSql.Visible = false;
            //
            // lblSqlServer
            //
            this.lblSqlServer.Location = new System.Drawing.Point(10, 22);
            this.lblSqlServer.Name = "lblSqlServer";
            this.lblSqlServer.Size = new System.Drawing.Size(140, 22);
            this.lblSqlServer.TabIndex = 0;
            this.lblSqlServer.Text = "Servidor SQL:";
            this.lblSqlServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtSqlServer
            //
            this.txtSqlServer.Location = new System.Drawing.Point(155, 22);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.Size = new System.Drawing.Size(280, 20);
            this.txtSqlServer.TabIndex = 1;
            //
            // lblSqlDatabase
            //
            this.lblSqlDatabase.Location = new System.Drawing.Point(10, 50);
            this.lblSqlDatabase.Name = "lblSqlDatabase";
            this.lblSqlDatabase.Size = new System.Drawing.Size(140, 22);
            this.lblSqlDatabase.TabIndex = 2;
            this.lblSqlDatabase.Text = "Banco de Dados:";
            this.lblSqlDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtSqlDatabase
            //
            this.txtSqlDatabase.Location = new System.Drawing.Point(155, 50);
            this.txtSqlDatabase.Name = "txtSqlDatabase";
            this.txtSqlDatabase.Size = new System.Drawing.Size(280, 20);
            this.txtSqlDatabase.TabIndex = 3;
            //
            // chkWindowsAuth
            //
            this.chkWindowsAuth.Location = new System.Drawing.Point(155, 78);
            this.chkWindowsAuth.Name = "chkWindowsAuth";
            this.chkWindowsAuth.Size = new System.Drawing.Size(300, 22);
            this.chkWindowsAuth.TabIndex = 4;
            this.chkWindowsAuth.Text = "Usar Autenticação Windows";
            this.chkWindowsAuth.CheckedChanged += new System.EventHandler(this.ChkWindowsAuth_CheckedChanged);
            //
            // lblSqlUsername
            //
            this.lblSqlUsername.Location = new System.Drawing.Point(10, 106);
            this.lblSqlUsername.Name = "lblSqlUsername";
            this.lblSqlUsername.Size = new System.Drawing.Size(140, 22);
            this.lblSqlUsername.TabIndex = 5;
            this.lblSqlUsername.Text = "Usuário SQL:";
            this.lblSqlUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtSqlUsername
            //
            this.txtSqlUsername.Location = new System.Drawing.Point(155, 106);
            this.txtSqlUsername.Name = "txtSqlUsername";
            this.txtSqlUsername.Size = new System.Drawing.Size(280, 20);
            this.txtSqlUsername.TabIndex = 6;
            //
            // lblSqlPassword
            //
            this.lblSqlPassword.Location = new System.Drawing.Point(10, 134);
            this.lblSqlPassword.Name = "lblSqlPassword";
            this.lblSqlPassword.Size = new System.Drawing.Size(140, 22);
            this.lblSqlPassword.TabIndex = 7;
            this.lblSqlPassword.Text = "Senha SQL:";
            this.lblSqlPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtSqlPassword
            //
            this.txtSqlPassword.Location = new System.Drawing.Point(155, 134);
            this.txtSqlPassword.Name = "txtSqlPassword";
            this.txtSqlPassword.PasswordChar = '*';
            this.txtSqlPassword.Size = new System.Drawing.Size(280, 20);
            this.txtSqlPassword.TabIndex = 8;
            //
            // btnTestSqlConnection
            //
            this.btnTestSqlConnection.Location = new System.Drawing.Point(155, 162);
            this.btnTestSqlConnection.Name = "btnTestSqlConnection";
            this.btnTestSqlConnection.Size = new System.Drawing.Size(120, 22);
            this.btnTestSqlConnection.TabIndex = 9;
            this.btnTestSqlConnection.Text = "Testar Conexão SQL";
            this.btnTestSqlConnection.Click += new System.EventHandler(this.BtnTestSqlConnection_Click);
            //
            // lblBackupFolder
            //
            this.lblBackupFolder.Location = new System.Drawing.Point(10, 194);
            this.lblBackupFolder.Name = "lblBackupFolder";
            this.lblBackupFolder.Size = new System.Drawing.Size(140, 22);
            this.lblBackupFolder.TabIndex = 10;
            this.lblBackupFolder.Text = "Pasta de Backup:";
            this.lblBackupFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtBackupFolder
            //
            this.txtBackupFolder.Location = new System.Drawing.Point(155, 194);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.Size = new System.Drawing.Size(240, 20);
            this.txtBackupFolder.TabIndex = 11;
            //
            // btnBrowseFolder
            //
            this.btnBrowseFolder.Location = new System.Drawing.Point(398, 193);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(28, 22);
            this.btnBrowseFolder.TabIndex = 12;
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            //
            // btnTestFolderPerm
            //
            this.btnTestFolderPerm.Location = new System.Drawing.Point(429, 193);
            this.btnTestFolderPerm.Name = "btnTestFolderPerm";
            this.btnTestFolderPerm.Size = new System.Drawing.Size(46, 22);
            this.btnTestFolderPerm.TabIndex = 13;
            this.btnTestFolderPerm.Text = "Testar";
            this.btnTestFolderPerm.Click += new System.EventHandler(this.BtnTestFolderPerm_Click);
            //
            // chkDeleteAfterUpload
            //
            this.chkDeleteAfterUpload.Location = new System.Drawing.Point(155, 222);
            this.chkDeleteAfterUpload.Name = "chkDeleteAfterUpload";
            this.chkDeleteAfterUpload.Size = new System.Drawing.Size(250, 22);
            this.chkDeleteAfterUpload.TabIndex = 14;
            this.chkDeleteAfterUpload.Text = "Deletar backup após upload";
            //
            // chkDeleteOnFailure
            //
            this.chkDeleteOnFailure.Location = new System.Drawing.Point(155, 250);
            this.chkDeleteOnFailure.Name = "chkDeleteOnFailure";
            this.chkDeleteOnFailure.Size = new System.Drawing.Size(330, 22);
            this.chkDeleteOnFailure.TabIndex = 15;
            this.chkDeleteOnFailure.Text = "Deletar backup em caso de falha no Upload";
            //
            // lblBackupFileName
            //
            this.lblBackupFileName.Location = new System.Drawing.Point(10, 278);
            this.lblBackupFileName.Name = "lblBackupFileName";
            this.lblBackupFileName.Size = new System.Drawing.Size(140, 22);
            this.lblBackupFileName.TabIndex = 16;
            this.lblBackupFileName.Text = "Nome do Arquivo:";
            this.lblBackupFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // txtBackupFileName
            //
            this.txtBackupFileName.Location = new System.Drawing.Point(155, 278);
            this.txtBackupFileName.Name = "txtBackupFileName";
            this.txtBackupFileName.Size = new System.Drawing.Size(240, 20);
            this.txtBackupFileName.TabIndex = 17;
            //
            // btnSave
            //
            this.btnSave.Location = new System.Drawing.Point(310, 660);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 33;
            this.btnSave.Text = "Salvar";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            //
            // btnCancel
            //
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(406, 660);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 28);
            this.btnCancel.TabIndex = 34;
            this.btnCancel.Text = "Cancelar";
            //
            // ConfigForm
            //
            this.ClientSize = new System.Drawing.Size(520, 700);
            this.Controls.Add(this.lblFileSection);
            this.Controls.Add(this.lblUploadFile);
            this.Controls.Add(this.txtUploadFilePath);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.lblDestinoSection);
            this.Controls.Add(this.rbAuthMega);
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
            this.Controls.Add(this.lblMegaEmail);
            this.Controls.Add(this.txtMegaEmail);
            this.Controls.Add(this.lblMegaPassword);
            this.Controls.Add(this.txtMegaPassword);
            this.Controls.Add(this.btnTestMegaLogin);
            this.Controls.Add(this.lblMegaFolder);
            this.Controls.Add(this.txtMegaFolder);
            this.Controls.Add(this.btnSelectMegaFolder);
            this.Controls.Add(this.lblFolderLink);
            this.Controls.Add(this.txtFolderLink);
            this.Controls.Add(this.lblScheduleSection);
            this.Controls.Add(this.lblScheduleTime);
            this.Controls.Add(this.dtpSchedule);
            this.Controls.Add(this.btnScheduleAction);
            this.Controls.Add(this.chkBackupMode);
            this.Controls.Add(this.grpBackupSql);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações - Agendador de Upload";
            this.grpBackupSql.ResumeLayout(false);
            this.grpBackupSql.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
