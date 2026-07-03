using System;
using System.Windows.Forms;
using AgendadorDeUpload.Models;
using AgendadorDeUpload.Security;
using AgendadorDeUpload.Services;
using AgendadorDeUpload.Forms;

namespace AgendadorDeUpload
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                LogService.Initialize();
                LogService.Write("Aplicação iniciada");

                var settingsPath = SecureStorage.GetDefaultSettingsPath();
                var encrypted = SecureStorage.LoadFromFile(settingsPath);

                if (encrypted != null)
                {
                    using (var prompt = new PasswordPromptForm())
                    {
                        if (prompt.ShowDialog() != DialogResult.OK)
                        {
                            LogService.Write("Autenticação cancelada. Encerrando.");
                            return;
                        }

                        var json = SecureStorage.Decrypt(encrypted, prompt.Password);
                        if (json == null)
                        {
                            MessageBox.Show("Senha incorreta.", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        AppState.MasterPassword = prompt.Password;
                    }
                }
                else
                {
                    using (var configForm = new ConfigForm(new BackupConfig(), null))
                    {
                        if (configForm.ShowDialog() != DialogResult.OK)
                        {
                            LogService.Write("Configuração cancelada. Encerrando.");
                            return;
                        }
                        AppState.MasterPassword = configForm.SavedPassword;
                    }
                }

                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                LogService.WriteError("Erro fatal", ex);
                MessageBox.Show($"Erro ao iniciar:\n{ex.Message}", "Erro",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
