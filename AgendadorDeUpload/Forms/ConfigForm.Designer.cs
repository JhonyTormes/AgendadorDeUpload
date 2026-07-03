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
        private System.Windows.Forms.Button btnTestFolderPerm;
        private System.Windows.Forms.Button btnTestMegaLogin;
        private System.Windows.Forms.CheckBox chkDeleteAfterUpload;
        private System.Windows.Forms.CheckBox chkDeleteOnFailure;
        private System.Windows.Forms.Label lblBackupFileName;
        private System.Windows.Forms.TextBox txtBackupFileName;
        private System.Windows.Forms.Label lblGDriveSection;
        private System.Windows.Forms.Label lblServiceAccount;
        private System.Windows.Forms.TextBox txtServiceAccountPath;
        private System.Windows.Forms.Button btnSelectJson;
        private System.Windows.Forms.Label lblDriveFolderId;
        private System.Windows.Forms.TextBox txtDriveFolderId;
        private System.Windows.Forms.Label lblScheduleSection;
        private System.Windows.Forms.Label lblScheduleTime;
        private System.Windows.Forms.DateTimePicker dtpSchedule;
        private System.Windows.Forms.Button btnScheduleAction;
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
        private System.Windows.Forms.Label lblMegaFolderId;
        private System.Windows.Forms.TextBox txtMegaFolderId;
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
            this.btnScheduleAction = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbAuthMega = new System.Windows.Forms.RadioButton();
            this.rbAuthServiceAccount = new System.Windows.Forms.RadioButton();
            this.rbAuthOAuth = new System.Windows.Forms.RadioButton();
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
            this.btnTestSqlConnection = new System.Windows.Forms.Button();
            this.btnTestFolderPerm = new System.Windows.Forms.Button();
            this.btnTestMegaLogin = new System.Windows.Forms.Button();
            this.chkDeleteAfterUpload = new System.Windows.Forms.CheckBox();
            this.chkDeleteOnFailure = new System.Windows.Forms.CheckBox();
            this.lblBackupFileName = new System.Windows.Forms.Label();
            this.txtBackupFileName = new System.Windows.Forms.TextBox();
            this.lblMegaFolder = new System.Windows.Forms.Label();
            this.txtMegaFolder = new System.Windows.Forms.TextBox();
            this.btnSelectMegaFolder = new System.Windows.Forms.Button();
            this.lblMegaFolderId = new System.Windows.Forms.Label();
            this.txtMegaFolderId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSqlServer
            // 
            this.lblSqlServer.Location = new System.Drawing.Point(10, 14);
            this.lblSqlServer.Name = "lblSqlServer";
            this.lblSqlServer.Size = new System.Drawing.Size(140, 22);
            this.lblSqlServer.TabIndex = 0;
            this.lblSqlServer.Text = "Servidor SQL:";
            this.lblSqlServer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSqlServer
            // 
            this.txtSqlServer.Location = new System.Drawing.Point(155, 14);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.Size = new System.Drawing.Size(330, 20);
            this.txtSqlServer.TabIndex = 1;
            // 
            // lblSqlDatabase
            // 
            this.lblSqlDatabase.Location = new System.Drawing.Point(10, 42);
            this.lblSqlDatabase.Name = "lblSqlDatabase";
            this.lblSqlDatabase.Size = new System.Drawing.Size(140, 22);
            this.lblSqlDatabase.TabIndex = 2;
            this.lblSqlDatabase.Text = "Banco de Dados:";
            this.lblSqlDatabase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSqlDatabase
            // 
            this.txtSqlDatabase.Location = new System.Drawing.Point(155, 42);
            this.txtSqlDatabase.Name = "txtSqlDatabase";
            this.txtSqlDatabase.Size = new System.Drawing.Size(330, 20);
            this.txtSqlDatabase.TabIndex = 3;
            // 
            // chkWindowsAuth
            // 
            this.chkWindowsAuth.Location = new System.Drawing.Point(155, 70);
            this.chkWindowsAuth.Name = "chkWindowsAuth";
            this.chkWindowsAuth.Size = new System.Drawing.Size(300, 22);
            this.chkWindowsAuth.TabIndex = 4;
            this.chkWindowsAuth.Text = "Usar Autenticação Windows";
            this.chkWindowsAuth.CheckedChanged += new System.EventHandler(this.ChkWindowsAuth_CheckedChanged);
            // 
            // lblSqlUsername
            // 
            this.lblSqlUsername.Location = new System.Drawing.Point(10, 98);
            this.lblSqlUsername.Name = "lblSqlUsername";
            this.lblSqlUsername.Size = new System.Drawing.Size(140, 22);
            this.lblSqlUsername.TabIndex = 5;
            this.lblSqlUsername.Text = "Usuário SQL:";
            this.lblSqlUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSqlUsername
            // 
            this.txtSqlUsername.Location = new System.Drawing.Point(155, 98);
            this.txtSqlUsername.Name = "txtSqlUsername";
            this.txtSqlUsername.Size = new System.Drawing.Size(330, 20);
            this.txtSqlUsername.TabIndex = 6;
            // 
            // lblSqlPassword
            // 
            this.lblSqlPassword.Location = new System.Drawing.Point(10, 126);
            this.lblSqlPassword.Name = "lblSqlPassword";
            this.lblSqlPassword.Size = new System.Drawing.Size(140, 22);
            this.lblSqlPassword.TabIndex = 7;
            this.lblSqlPassword.Text = "Senha SQL:";
            this.lblSqlPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSqlPassword
            // 
            this.txtSqlPassword.Location = new System.Drawing.Point(155, 126);
            this.txtSqlPassword.Name = "txtSqlPassword";
            this.txtSqlPassword.PasswordChar = '*';
            this.txtSqlPassword.Size = new System.Drawing.Size(330, 20);
            this.txtSqlPassword.TabIndex = 8;
            // 
            // lblBackupSection
            // 
            this.lblBackupSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBackupSection.Location = new System.Drawing.Point(10, 182);
            this.lblBackupSection.Name = "lblBackupSection";
            this.lblBackupSection.Size = new System.Drawing.Size(480, 20);
            this.lblBackupSection.TabIndex = 10;
            this.lblBackupSection.Text = "Backup";
            // 
            // lblBackupFolder
            // 
            this.lblBackupFolder.Location = new System.Drawing.Point(10, 206);
            this.lblBackupFolder.Name = "lblBackupFolder";
            this.lblBackupFolder.Size = new System.Drawing.Size(140, 22);
            this.lblBackupFolder.TabIndex = 11;
            this.lblBackupFolder.Text = "Pasta de Backup:";
            this.lblBackupFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBackupFolder
            // 
            this.txtBackupFolder.Location = new System.Drawing.Point(155, 206);
            this.txtBackupFolder.Name = "txtBackupFolder";
            this.txtBackupFolder.Size = new System.Drawing.Size(250, 20);
            this.txtBackupFolder.TabIndex = 12;
            // 
            // btnBrowseFolder
            // 
            this.btnBrowseFolder.Location = new System.Drawing.Point(408, 205);
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.Size = new System.Drawing.Size(28, 22);
            this.btnBrowseFolder.TabIndex = 13;
            this.btnBrowseFolder.Text = "...";
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // lblGDriveSection
            // 
            this.lblGDriveSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblGDriveSection.Location = new System.Drawing.Point(10, 275);
            this.lblGDriveSection.Name = "lblGDriveSection";
            this.lblGDriveSection.Size = new System.Drawing.Size(486, 43);
            this.lblGDriveSection.TabIndex = 16;
            this.lblGDriveSection.Text = "Destino do Upload";
            // 
            // lblServiceAccount
            // 
            this.lblServiceAccount.Location = new System.Drawing.Point(10, 342);
            this.lblServiceAccount.Name = "lblServiceAccount";
            this.lblServiceAccount.Size = new System.Drawing.Size(140, 22);
            this.lblServiceAccount.TabIndex = 19;
            this.lblServiceAccount.Text = "Arquivo JSON:";
            this.lblServiceAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServiceAccountPath
            // 
            this.txtServiceAccountPath.Location = new System.Drawing.Point(155, 342);
            this.txtServiceAccountPath.Name = "txtServiceAccountPath";
            this.txtServiceAccountPath.ReadOnly = true;
            this.txtServiceAccountPath.Size = new System.Drawing.Size(300, 20);
            this.txtServiceAccountPath.TabIndex = 20;
            // 
            // btnSelectJson
            // 
            this.btnSelectJson.Location = new System.Drawing.Point(457, 341);
            this.btnSelectJson.Name = "btnSelectJson";
            this.btnSelectJson.Size = new System.Drawing.Size(28, 22);
            this.btnSelectJson.TabIndex = 21;
            this.btnSelectJson.Text = "...";
            this.btnSelectJson.Click += new System.EventHandler(this.BtnSelectJson_Click);
            // 
            // lblDriveFolderId
            // 
            this.lblDriveFolderId.Location = new System.Drawing.Point(10, 430);
            this.lblDriveFolderId.Name = "lblDriveFolderId";
            this.lblDriveFolderId.Size = new System.Drawing.Size(140, 22);
            this.lblDriveFolderId.TabIndex = 36;
            this.lblDriveFolderId.Text = "Pasta ID:";
            this.lblDriveFolderId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDriveFolderId
            // 
            this.txtDriveFolderId.Location = new System.Drawing.Point(155, 430);
            this.txtDriveFolderId.Name = "txtDriveFolderId";
            this.txtDriveFolderId.Size = new System.Drawing.Size(330, 20);
            this.txtDriveFolderId.TabIndex = 37;
            // 
            // lblScheduleSection
            // 
            this.lblScheduleSection.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblScheduleSection.Location = new System.Drawing.Point(10, 466);
            this.lblScheduleSection.Name = "lblScheduleSection";
            this.lblScheduleSection.Size = new System.Drawing.Size(480, 20);
            this.lblScheduleSection.TabIndex = 38;
            this.lblScheduleSection.Text = "Agendamento";
            // 
            // lblScheduleTime
            // 
            this.lblScheduleTime.Location = new System.Drawing.Point(10, 490);
            this.lblScheduleTime.Name = "lblScheduleTime";
            this.lblScheduleTime.Size = new System.Drawing.Size(140, 22);
            this.lblScheduleTime.TabIndex = 39;
            this.lblScheduleTime.Text = "Data e Hora:";
            this.lblScheduleTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSchedule
            // 
            this.dtpSchedule.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpSchedule.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSchedule.Location = new System.Drawing.Point(155, 490);
            this.dtpSchedule.Name = "dtpSchedule";
            this.dtpSchedule.Size = new System.Drawing.Size(140, 20);
            this.dtpSchedule.TabIndex = 40;
            // 
            // btnScheduleAction
            // 
            this.btnScheduleAction.Location = new System.Drawing.Point(300, 488);
            this.btnScheduleAction.Name = "btnScheduleAction";
            this.btnScheduleAction.Size = new System.Drawing.Size(110, 24);
            this.btnScheduleAction.TabIndex = 41;
            this.btnScheduleAction.Text = "Agendar";
            this.btnScheduleAction.Click += new System.EventHandler(this.BtnScheduleAction_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(310, 550);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 28);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Salvar";
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(406, 550);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 28);
            this.btnCancel.TabIndex = 47;
            this.btnCancel.Text = "Cancelar";
            // 
            // rbAuthMega
            // 
            this.rbAuthMega.Location = new System.Drawing.Point(155, 318);
            this.rbAuthMega.Name = "rbAuthMega";
            this.rbAuthMega.Size = new System.Drawing.Size(70, 22);
            this.rbAuthMega.TabIndex = 17;
            this.rbAuthMega.Text = "Mega";
            this.rbAuthMega.CheckedChanged += new System.EventHandler(this.RbAuthMethod_CheckedChanged);
            // 
            // rbAuthServiceAccount
            // 
            this.rbAuthServiceAccount.Location = new System.Drawing.Point(230, 318);
            this.rbAuthServiceAccount.Name = "rbAuthServiceAccount";
            this.rbAuthServiceAccount.Size = new System.Drawing.Size(158, 22);
            this.rbAuthServiceAccount.TabIndex = 18;
            this.rbAuthServiceAccount.Text = "Drive (Conta de Serviço)";
            this.rbAuthServiceAccount.CheckedChanged += new System.EventHandler(this.RbAuthMethod_CheckedChanged);
            // 
            // rbAuthOAuth
            // 
            this.rbAuthOAuth.Location = new System.Drawing.Point(391, 318);
            this.rbAuthOAuth.Name = "rbAuthOAuth";
            this.rbAuthOAuth.Size = new System.Drawing.Size(100, 22);
            this.rbAuthOAuth.TabIndex = 19;
            this.rbAuthOAuth.Text = "Drive (OAuth)";
            this.rbAuthOAuth.CheckedChanged += new System.EventHandler(this.RbAuthMethod_CheckedChanged);
            // 
            // lblOAuthClientId
            // 
            this.lblOAuthClientId.Location = new System.Drawing.Point(0, 0);
            this.lblOAuthClientId.Name = "lblOAuthClientId";
            this.lblOAuthClientId.Size = new System.Drawing.Size(100, 23);
            this.lblOAuthClientId.TabIndex = 22;
            // 
            // txtOAuthClientId
            // 
            this.txtOAuthClientId.Location = new System.Drawing.Point(0, 0);
            this.txtOAuthClientId.Name = "txtOAuthClientId";
            this.txtOAuthClientId.Size = new System.Drawing.Size(100, 20);
            this.txtOAuthClientId.TabIndex = 23;
            // 
            // lblOAuthClientSecret
            // 
            this.lblOAuthClientSecret.Location = new System.Drawing.Point(0, 0);
            this.lblOAuthClientSecret.Name = "lblOAuthClientSecret";
            this.lblOAuthClientSecret.Size = new System.Drawing.Size(100, 23);
            this.lblOAuthClientSecret.TabIndex = 24;
            // 
            // txtOAuthClientSecret
            // 
            this.txtOAuthClientSecret.Location = new System.Drawing.Point(0, 0);
            this.txtOAuthClientSecret.Name = "txtOAuthClientSecret";
            this.txtOAuthClientSecret.Size = new System.Drawing.Size(100, 20);
            this.txtOAuthClientSecret.TabIndex = 25;
            // 
            // btnAuthorizeOAuth
            // 
            this.btnAuthorizeOAuth.Location = new System.Drawing.Point(0, 0);
            this.btnAuthorizeOAuth.Name = "btnAuthorizeOAuth";
            this.btnAuthorizeOAuth.Size = new System.Drawing.Size(75, 23);
            this.btnAuthorizeOAuth.TabIndex = 26;
            // 
            // lblOAuthStatus
            // 
            this.lblOAuthStatus.Location = new System.Drawing.Point(0, 0);
            this.lblOAuthStatus.Name = "lblOAuthStatus";
            this.lblOAuthStatus.Size = new System.Drawing.Size(100, 23);
            this.lblOAuthStatus.TabIndex = 27;
            // 
            // lblMegaEmail
            // 
            this.lblMegaEmail.Location = new System.Drawing.Point(10, 342);
            this.lblMegaEmail.Name = "lblMegaEmail";
            this.lblMegaEmail.Size = new System.Drawing.Size(140, 22);
            this.lblMegaEmail.TabIndex = 29;
            this.lblMegaEmail.Text = "E-mail Mega:";
            this.lblMegaEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMegaEmail
            // 
            this.txtMegaEmail.Location = new System.Drawing.Point(155, 342);
            this.txtMegaEmail.Name = "txtMegaEmail";
            this.txtMegaEmail.Size = new System.Drawing.Size(330, 20);
            this.txtMegaEmail.TabIndex = 30;
            // 
            // lblMegaPassword
            // 
            this.lblMegaPassword.Location = new System.Drawing.Point(10, 370);
            this.lblMegaPassword.Name = "lblMegaPassword";
            this.lblMegaPassword.Size = new System.Drawing.Size(140, 22);
            this.lblMegaPassword.TabIndex = 31;
            this.lblMegaPassword.Text = "Senha Mega:";
            this.lblMegaPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMegaPassword
            // 
            this.txtMegaPassword.Location = new System.Drawing.Point(155, 370);
            this.txtMegaPassword.Name = "txtMegaPassword";
            this.txtMegaPassword.PasswordChar = '*';
            this.txtMegaPassword.Size = new System.Drawing.Size(250, 20);
            this.txtMegaPassword.TabIndex = 32;
            // 
            // btnTestSqlConnection
            // 
            this.btnTestSqlConnection.Location = new System.Drawing.Point(155, 150);
            this.btnTestSqlConnection.Name = "btnTestSqlConnection";
            this.btnTestSqlConnection.Size = new System.Drawing.Size(120, 22);
            this.btnTestSqlConnection.TabIndex = 9;
            this.btnTestSqlConnection.Text = "Testar Conexão SQL";
            this.btnTestSqlConnection.Click += new System.EventHandler(this.BtnTestSqlConnection_Click);
            // 
            // btnTestFolderPerm
            // 
            this.btnTestFolderPerm.Location = new System.Drawing.Point(439, 205);
            this.btnTestFolderPerm.Name = "btnTestFolderPerm";
            this.btnTestFolderPerm.Size = new System.Drawing.Size(46, 22);
            this.btnTestFolderPerm.TabIndex = 14;
            this.btnTestFolderPerm.Text = "Testar";
            this.btnTestFolderPerm.Click += new System.EventHandler(this.BtnTestFolderPerm_Click);
            // 
            // btnTestMegaLogin
            // 
            this.btnTestMegaLogin.Location = new System.Drawing.Point(410, 370);
            this.btnTestMegaLogin.Name = "btnTestMegaLogin";
            this.btnTestMegaLogin.Size = new System.Drawing.Size(75, 22);
            this.btnTestMegaLogin.TabIndex = 33;
            this.btnTestMegaLogin.Text = "Testar";
            this.btnTestMegaLogin.Click += new System.EventHandler(this.BtnTestMegaLogin_Click);
            // 
            // chkDeleteAfterUpload
            // 
            this.chkDeleteAfterUpload.Location = new System.Drawing.Point(155, 235);
            this.chkDeleteAfterUpload.Name = "chkDeleteAfterUpload";
            this.chkDeleteAfterUpload.Size = new System.Drawing.Size(250, 22);
            this.chkDeleteAfterUpload.TabIndex = 15;
            this.chkDeleteAfterUpload.Text = "Deletar backup após upload";
            // 
            // chkDeleteOnFailure
            // 
            this.chkDeleteOnFailure.Location = new System.Drawing.Point(155, 258);
            this.chkDeleteOnFailure.Name = "chkDeleteOnFailure";
            this.chkDeleteOnFailure.Size = new System.Drawing.Size(330, 22);
            this.chkDeleteOnFailure.TabIndex = 16;
            this.chkDeleteOnFailure.Text = "Deletar backup em caso de falha no Upload";
            // 
            // lblBackupFileName
            // 
            this.lblBackupFileName.Location = new System.Drawing.Point(10, 293);
            this.lblBackupFileName.Name = "lblBackupFileName";
            this.lblBackupFileName.Size = new System.Drawing.Size(140, 22);
            this.lblBackupFileName.TabIndex = 17;
            this.lblBackupFileName.Text = "Nome do Arquivo:";
            this.lblBackupFileName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBackupFileName
            // 
            this.txtBackupFileName.Location = new System.Drawing.Point(155, 293);
            this.txtBackupFileName.Name = "txtBackupFileName";
            this.txtBackupFileName.Size = new System.Drawing.Size(250, 20);
            this.txtBackupFileName.TabIndex = 18;
            // 
            // lblMegaFolder
            // 
            this.lblMegaFolder.Location = new System.Drawing.Point(10, 398);
            this.lblMegaFolder.Name = "lblMegaFolder";
            this.lblMegaFolder.Size = new System.Drawing.Size(140, 22);
            this.lblMegaFolder.TabIndex = 33;
            this.lblMegaFolder.Text = "Pasta Mega:";
            this.lblMegaFolder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMegaFolder
            // 
            this.txtMegaFolder.Location = new System.Drawing.Point(155, 398);
            this.txtMegaFolder.Name = "txtMegaFolder";
            this.txtMegaFolder.ReadOnly = true;
            this.txtMegaFolder.Size = new System.Drawing.Size(300, 20);
            this.txtMegaFolder.TabIndex = 34;
            // 
            // btnSelectMegaFolder
            // 
            this.btnSelectMegaFolder.Location = new System.Drawing.Point(457, 397);
            this.btnSelectMegaFolder.Name = "btnSelectMegaFolder";
            this.btnSelectMegaFolder.Size = new System.Drawing.Size(28, 22);
            this.btnSelectMegaFolder.TabIndex = 35;
            this.btnSelectMegaFolder.Text = "...";
            this.btnSelectMegaFolder.Click += new System.EventHandler(this.BtnSelectMegaFolder_Click);
            // 
            // lblMegaFolderId
            // 
            this.lblMegaFolderId.Location = new System.Drawing.Point(10, 425);
            this.lblMegaFolderId.Name = "lblMegaFolderId";
            this.lblMegaFolderId.Size = new System.Drawing.Size(140, 22);
            this.lblMegaFolderId.TabIndex = 36;
            this.lblMegaFolderId.Text = "ID Pasta Compartilhada:";
            this.lblMegaFolderId.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMegaFolderId
            // 
            this.txtMegaFolderId.Location = new System.Drawing.Point(155, 425);
            this.txtMegaFolderId.Name = "txtMegaFolderId";
            this.txtMegaFolderId.Size = new System.Drawing.Size(330, 20);
            this.txtMegaFolderId.TabIndex = 37;
            // 
            // ConfigForm
            // 
            this.ClientSize = new System.Drawing.Size(520, 590);
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
            this.Controls.Add(this.btnTestFolderPerm);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.chkDeleteAfterUpload);
            this.Controls.Add(this.chkDeleteOnFailure);
            this.Controls.Add(this.lblBackupFileName);
            this.Controls.Add(this.txtBackupFileName);
            this.Controls.Add(this.lblGDriveSection);
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
            this.Controls.Add(this.lblMegaFolderId);
            this.Controls.Add(this.txtMegaFolderId);
            this.Controls.Add(this.lblDriveFolderId);
            this.Controls.Add(this.txtDriveFolderId);
            this.Controls.Add(this.lblScheduleSection);
            this.Controls.Add(this.lblScheduleTime);
            this.Controls.Add(this.dtpSchedule);
            this.Controls.Add(this.btnScheduleAction);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configurações - Agendador de Upload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
