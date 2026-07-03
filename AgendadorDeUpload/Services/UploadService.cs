using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AgendadorDeUpload.Services
{
    public class UploadService
    {
        private readonly string _authMethod;
        private readonly string _serviceAccountJson;
        private readonly string _oauthClientId;
        private readonly string _oauthClientSecret;
        private readonly string _oauthRefreshToken;
        private readonly string _folderId;

        public UploadService(
            string authMethod,
            string serviceAccountJson,
            string oauthClientId,
            string oauthClientSecret,
            string oauthRefreshToken,
            string folderId)
        {
            _authMethod = authMethod;
            _serviceAccountJson = serviceAccountJson;
            _oauthClientId = oauthClientId;
            _oauthClientSecret = oauthClientSecret;
            _oauthRefreshToken = oauthRefreshToken;
            _folderId = folderId;
        }

        public string Upload(string filePath, Action<string> onProgress = null)
        {
            LogService.Write("Autenticando no Google Drive...");
            onProgress?.Invoke("Autenticando no Google Drive...");

            BaseClientService.Initializer initializer;

            if (_authMethod == "OAuth")
            {
                var token = new Google.Apis.Auth.OAuth2.Responses.TokenResponse
                {
                    RefreshToken = _oauthRefreshToken
                };

                var credential = new UserCredential(
                    new GoogleAuthorizationCodeFlow(
                        new GoogleAuthorizationCodeFlow.Initializer
                        {
                            ClientSecrets = new ClientSecrets
                            {
                                ClientId = _oauthClientId,
                                ClientSecret = _oauthClientSecret
                            },
                            Scopes = new[] { DriveService.Scope.Drive }
                        }),
                    "user",
                    token);

                initializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "AgendadorDeUpload"
                };
            }
            else
            {
                // Validação crítica para Service Account: impede tentativa de upload sem pasta alvo definida
                if (string.IsNullOrWhiteSpace(_folderId))
                {
                    throw new InvalidOperationException("Erro: O ID da pasta de destino (_folderId) não foi configurado. Service Accounts precisam de uma pasta pai compartilhada.");
                }

                GoogleCredential credential;
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(_serviceAccountJson)))
                {
                    // Correção 1: Inclusão combinada dos escopos Drive e DriveFile para garantir leitura de arquivos compartilhados
                    credential = GoogleCredential.FromStream(stream)
                        .CreateScoped(new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile });
                }

                initializer = new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "AgendadorDeUpload"
                };
            }

            var service = new DriveService(initializer);

            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string> { _folderId }
            };

            var fileName = Path.GetFileName(filePath);
            LogService.Write($"Upload iniciado: {fileName}");
            onProgress?.Invoke($"Enviando {fileName}...");

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Correção 2: Mudança de "application/octet-stream" para null para forçar o Google a processar os metadados do fileMetadata corretamente
                var request = service.Files.Create(fileMetadata, fs, null);
                request.Fields = "id, webViewLink";

                request.SupportsAllDrives = true;

                var progress = request.Upload();

                if (progress.Status == Google.Apis.Upload.UploadStatus.Failed)
                {
                    var erroReal = progress.Exception?.InnerException?.Message ?? progress.Exception?.Message;
                    throw new InvalidOperationException($"Falha no upload: {erroReal}");
                }

                var file = request.ResponseBody;
                if (file == null)
                    throw new InvalidOperationException("Resposta do Google Drive veio vazia. Verifique se a pasta de destino existe e a conta de serviço tem permissão de Editor.");

                LogService.Write($"Upload concluído. ID: {file.Id}");
                onProgress?.Invoke("Upload concluído com sucesso!");

                return file.Id;
            }
        }
    }
}
