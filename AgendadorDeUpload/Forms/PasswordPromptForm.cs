using System;
using System.Windows.Forms;

namespace AgendadorDeUpload.Forms
{
    public partial class PasswordPromptForm : Form
    {
        public string Password => txtPassword.Text;

        public PasswordPromptForm()
        {
            InitializeComponent();
            Icon = MainForm.LoadAppIcon();
        }

        private void InitializeComponent()
        {
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblMessage.Text = "Digite a senha de configuração:";
            this.lblMessage.Location = new System.Drawing.Point(12, 15);
            this.lblMessage.Size = new System.Drawing.Size(310, 20);

            this.txtPassword.Location = new System.Drawing.Point(12, 40);
            this.txtPassword.Size = new System.Drawing.Size(310, 22);
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) this.btnOk.PerformClick();
            };

            this.btnOk.Text = "OK";
            this.btnOk.Location = new System.Drawing.Point(166, 75);
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.DialogResult = DialogResult.OK;

            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Location = new System.Drawing.Point(247, 75);
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.DialogResult = DialogResult.Cancel;

            this.ClientSize = new System.Drawing.Size(350, 120);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Autenticação";

            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);

            this.AcceptButton = this.btnOk;
            this.CancelButton = this.btnCancel;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMessage;
    }
}
