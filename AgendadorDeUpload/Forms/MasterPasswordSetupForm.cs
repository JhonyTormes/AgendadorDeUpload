using System;
using System.Windows.Forms;

namespace AgendadorDeUpload.Forms
{
    public partial class MasterPasswordSetupForm : Form
    {
        public string SavedPassword { get; private set; }

        public MasterPasswordSetupForm()
        {
            InitializeComponent();
            Icon = MainForm.LoadAppIcon();
        }

        private void InitializeComponent()
        {
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();

            this.lblMessage.Text = "Crie uma senha mestra para proteger as configurações do aplicativo.\n\n"
                + "Ela será usada para criptografar:\n"
                + "• Credenciais do SQL Server\n"
                + "• Credenciais do Mega\n"
                + "• Tokens do Google Drive\n\n"
                + "Esta senha é definida uma única vez e não pode ser alterada.\n"
                + "Anote-a em um local seguro!";
            this.lblMessage.Location = new System.Drawing.Point(12, 12);
            this.lblMessage.Size = new System.Drawing.Size(380, 130);

            this.lblPassword.Text = "Senha Mestra:";
            this.lblPassword.Location = new System.Drawing.Point(12, 150);
            this.lblPassword.Size = new System.Drawing.Size(100, 22);
            this.lblPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtPassword.Location = new System.Drawing.Point(118, 150);
            this.txtPassword.Size = new System.Drawing.Size(270, 22);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) txtConfirm.Focus(); };

            this.lblConfirm.Text = "Confirmar:";
            this.lblConfirm.Location = new System.Drawing.Point(12, 180);
            this.lblConfirm.Size = new System.Drawing.Size(100, 22);
            this.lblConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.txtConfirm.Location = new System.Drawing.Point(118, 180);
            this.txtConfirm.Size = new System.Drawing.Size(270, 22);
            this.txtConfirm.PasswordChar = '*';
            this.txtConfirm.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) btnOk.PerformClick(); };

            this.btnOk.Text = "Criar Senha";
            this.btnOk.Location = new System.Drawing.Point(200, 215);
            this.btnOk.Size = new System.Drawing.Size(90, 28);
            this.btnOk.Click += BtnOk_Click;

            this.btnCancel.Text = "Sair";
            this.btnCancel.Location = new System.Drawing.Point(296, 215);
            this.btnCancel.Size = new System.Drawing.Size(90, 28);
            this.btnCancel.DialogResult = DialogResult.Cancel;

            this.ClientSize = new System.Drawing.Size(404, 260);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuração Inicial - Senha Mestra";
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblConfirm);
            this.Controls.Add(this.txtConfirm);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            var pw = txtPassword.Text;
            var confirm = txtConfirm.Text;

            if (string.IsNullOrWhiteSpace(pw))
            {
                MessageBox.Show(this, "Digite uma senha mestra.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pw != confirm)
            {
                MessageBox.Show(this, "As senhas não conferem.", "Validação",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SavedPassword = pw;
            DialogResult = DialogResult.OK;
            Close();
        }

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
    }
}
