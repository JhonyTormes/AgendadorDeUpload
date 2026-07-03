using System;
using System.Threading;
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
            bool createdNew;
            using (var mutex = new Mutex(true, "AgendadorDeUpload", out createdNew))
            {
                if (!createdNew)
                {
                    MessageBox.Show("O Agendador de Upload já está em execução.", "Aviso",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                RunApp();
            }
        }

        private static void RunApp()
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
                        AppState.LastAuthTime = DateTime.Now;
                    }
                }
                else
                {
                    using (var setupForm = new MasterPasswordSetupForm())
                    {
                        if (setupForm.ShowDialog() != DialogResult.OK)
                        {
                            LogService.Write("Configuração cancelada. Encerrando.");
                            return;
                        }
                        AppState.MasterPassword = setupForm.SavedPassword;
                        AppState.LastAuthTime = DateTime.Now;
                    }

                    using (var configForm = new ConfigForm(new BackupConfig(), AppState.MasterPassword))
                    {
                        if (configForm.ShowDialog() != DialogResult.OK)
                        {
                            LogService.Write("Configuração cancelada. Encerrando.");
                            return;
                        }
                        AppState.MasterPassword = configForm.SavedPassword;
                        AppState.LastAuthTime = DateTime.Now;
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
