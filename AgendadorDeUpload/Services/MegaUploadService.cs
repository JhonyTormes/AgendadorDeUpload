using System;
using System.IO;
using System.Linq;
using System.Threading;
using CG.Web.MegaApiClient;

namespace AgendadorDeUpload.Services
{
    public class MegaUploadService
    {
        private readonly string _email;
        private readonly string _password;
        private readonly string _remoteFolder;
        private readonly string _remoteFolderId;

        public MegaUploadService(string email, string password, string remoteFolder = null, string remoteFolderId = null)
        {
            _email = email;
            _password = password;
            _remoteFolder = remoteFolder;
            _remoteFolderId = remoteFolderId;
        }

        public string Upload(string filePath, Action<string> onProgress = null, CancellationToken ct = default)
        {
            LogService.Write("Autenticando no Mega...");
            onProgress?.Invoke("Autenticando no Mega...");

            var client = new MegaApiClient();
            client.Login(_email, _password);

            try
            {
                var nodes = client.GetNodes().ToArray();
                var parent = ResolveParentNode(client, nodes);

                var fileName = Path.GetFileName(filePath);
                LogService.Write($"Upload iniciado: {fileName}");
                onProgress?.Invoke($"Enviando {fileName}...");

                var startTime = DateTime.UtcNow;
                INode uploaded;

                using (var fileStream = File.OpenRead(filePath))
                {
                    var progressStream = new ProgressStream(fileStream, fileStream.Length, progress =>
                    {
                        var pct = (int)(progress * 100);
                        var elapsed = DateTime.UtcNow - startTime;
                        string eta;
                        if (progress > 0.01)
                        {
                            var totalEst = TimeSpan.FromTicks((long)(elapsed.Ticks / progress));
                            var remaining = totalEst - elapsed;
                            eta = remaining.TotalMinutes >= 1
                                ? $"{remaining.Minutes}m{remaining.Seconds}s"
                                : $"{remaining.Seconds}s";
                        }
                        else
                        {
                            eta = "calculando...";
                        }

                        var msg = $"{pct}% (tempo restante estimado: {eta})";
                        LogService.Write(msg);
                        onProgress?.Invoke(msg);
                    });

                    uploaded = client.Upload(progressStream, fileName, parent, null, ct);
                }

                string link = null;
                try
                {
                    link = client.GetDownloadLink(uploaded).ToString();
                }
                catch (Exception ex)
                {
                    LogService.Write($"Aviso: não foi possível obter link público ({ex.Message})");
                }

                LogService.Write($"Upload concluído: {link ?? "sem link público"}");
                onProgress?.Invoke("Upload concluído com sucesso!");

                return link ?? "ok";
            }
            finally
            {
                client.Logout();
            }
        }

        private INode ResolveParentNode(MegaApiClient client, INode[] nodes)
        {
            if (!string.IsNullOrWhiteSpace(_remoteFolderId))
            {
                var byId = nodes.FirstOrDefault(n =>
                    n.Type == NodeType.Directory && string.Equals(n.Id, _remoteFolderId, StringComparison.OrdinalIgnoreCase));
                if (byId != null)
                    return byId;
            }

            if (!string.IsNullOrWhiteSpace(_remoteFolder))
            {
                var byName = nodes.FirstOrDefault(n =>
                    n.Type == NodeType.Directory && string.Equals(n.Name, _remoteFolder, StringComparison.OrdinalIgnoreCase));
                if (byName != null)
                    return byName;
            }

            var root = nodes.FirstOrDefault(n => n.Type == NodeType.Root);
            return root ?? nodes.First();
        }
    }
}
