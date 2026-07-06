# Agendador de Upload

Aplicação Windows Forms (.NET Framework 4.8) que gera backups de bancos SQL Server em horário agendado, monitora a estabilidade do arquivo, faz upload para Mega.nz ou Google Drive, e auto-encerra.

## Funcionalidades

### Backup SQL Server
- Backup de banco SQL Server com `BACKUP DATABASE ... WITH COMPRESSION`
- Suporte a autenticação Windows e SQL Server
- Detecta se já há um backup em andamento no banco (`sys.dm_exec_requests`) antes de iniciar
- Permite cancelar o backup em execução.
- Teste de conexão com timeout de 5 segundos
- Teste de permissão de pasta: verifica escrita direta + escrita do SQL Server

### Agendamento
- Agendamento por data/hora única
- Botão "Agendar" / "Cancelar Agendamento" no formulário de configuração
- Alterar o horário no DateTimePicker sempre redefine o estado para "Agendar", exigindo confirmação explícita
- Timer verifica a cada 15s se o horário agendado chegou
- Ao salvar, persiste o horário agendado ou string vazia para desagendar
- Execução manual via menu "Executar backup agora"
- Modo `/run` para execução direta via Task Scheduler do Windows
- Marcadores de execução (`HasRun`) e falha (`HasFailed`) em `%LOCALAPPDATA%\AgendadorDeUpload\`
- Se um backup falhou na execução anterior, ao abrir o programa o agendamento é cancelado automaticamente e um aviso é mostrado

### Monitoramento de Arquivo
- Após o backup, monitora o arquivo a cada 5 segundos
- Considera estável após 30 segundos sem alteração de tamanho
- Exibe o tamanho atual durante o monitoramento

### Upload
- **Mega.nz**: autenticação por e-mail + senha, seleção de pasta via árvore (pastas próprias apenas, sem "Compartilhados comigo"), upload com progresso a cada 5% e tempo restante estimado, suporte a ID de pasta compartilhada manual, suporte a `CancellationToken`
- **Google Drive (Service Account)**: upload via Service Account JSON, requer pasta de destino compartilhada com a service account
- **Google Drive (OAuth)**: upload via OAuth 2.0 com refresh token, fluxo de autorização integrado
- Três métodos de autenticação exibidos como RadioButtons: Mega, Drive (Conta de Serviço), Drive (OAuth)
- Logs de progresso a cada 5% no formato `"45% (tempo restante estimado: 2m30s)"`

### Pós-Upload
- Opção "Deletar backup após upload"
- Opção "Deletar backup em caso de falha no Upload"
- Balloon notification de sucesso/erro
- Auto-exit 5 segundos após upload bem-sucedido

### Segurança
- **Senha Mestra**: criada no primeiro uso via `MasterPasswordSetupForm`; usada para criptografar todas as credenciais
- Criptografia AES-256 com PBKDF2 (300.000 iterações, SHA-256) + DPAPI (ProtectedData)
- Salt aleatório por instalação (16 bytes gerados via `RandomNumberGenerator`), armazenado inline no arquivo criptografado
- Cache de senha por 1 minuto (`AppState.LastAuthTime`); após expirar, o prompt é exibido novamente ao abrir configurações ou executar manualmente
- Config salva em `settings.enc` no mesmo diretório do executável
- Hash da senha mestra via PBKDF2 (300.000 iterações, SHA-256) com comparação em tempo constante (`FixedTimeEquals`)

### Controle de Execução
- Execução assíncrona (`async Task`) com suporte a `CancellationToken`
- Botão "Cancelar" no menu do tray para interromper backup/upload em andamento
- Cancela backup SQL; cancela upload Mega via `CancellationToken`
- Idempotente: `AppState.IsRunning` impede segunda execução simultânea
- Se config form estiver aberto, não abre outra instância

### Interface
- Apenas ícone na bandeja (sem janela visível); duplo clique abre configurações
- Menu do tray: Configurações, Executar backup agora, Cancelar, Sair
- Status exibido no tooltip do tray icon (máx. 63 caracteres)

### Logs
- Log diário em `%EXEDIR%\Logs\log_YYYYMMDD.txt`
- Log de erros com stack trace e inner exception
- Inicializado antes de qualquer outra operação

## Arquitetura

### Projeto
- .NET Framework 4.8, Windows Forms, SDK-style `.csproj`
- Costura.Fody mergeia todas as DLLs em um único `.exe`
- Único assembly de saída: `AgendadorDeUpload.exe` + `AgendadorDeUpload.exe.config`

### Estrutura

```
AgendadorDeUpload/
├── Program.cs                    # Entry point, mutex, startup flow
├── AppState.cs                   # Estado global (senha, running, cancel source)
├── MainForm.cs                   # Tray icon, scheduler, fluxo de backup
├── envio.ico / envio.png         # Ícone
├── FodyWeavers.xml               # Costura.Fody config
│
├── Forms/
│   ├── ConfigForm.cs             # Formulário principal de configuração
│   ├── ConfigForm.Designer.cs    # Layout Visual Studio
│   ├── MasterPasswordSetupForm.cs # Criação da senha mestra (primeiro uso)
│   ├── PasswordPromptForm.cs     # Prompt de senha
│   └── MegaFolderPickerForm.cs   # Seletor de pastas Mega (árvore)
│
├── Models/
│   ├── BackupConfig.cs           # Modelo de configuração (JSON serializable)
│   └── BackupResult.cs           # Resultado do backup
│
├── Services/
│   ├── BackupService.cs          # SQL backup, IsBackupRunning, KillBackup
│   ├── FileMonitorService.cs     # Monitoramento de estabilidade do arquivo
│   ├── LogService.cs             # Log diário em arquivo
│   ├── MegaUploadService.cs      # Upload Mega com progresso
│   ├── ProgressStream.cs         # Stream wrapper para progresso a cada 5%
│   └── UploadService.cs          # Upload Google Drive (Service Account + OAuth)
│
├── Security/
│   ├── PasswordManager.cs        # PBKDF2 hash/verify
│   └── SecureStorage.cs          # AES-256 + DPAPI encrypt/decrypt
│
└── Scheduling/
    └── SchedulerService.cs       # Agendamento por data/hora, marcadores
```


## Configuração (ConfigForm)

| Campo | Descrição |
|---|---|
| Servidor SQL | Host/instância do SQL Server |
| Banco de Dados | Nome do banco para backup |
| Autenticação Windows | Checkbox para Integrated Security |
| Usuário SQL / Senha SQL | Credenciais SQL (desabilitado se Windows Auth) |
| Testar Conexão SQL | Botão de teste com timeout 5s |
| Pasta de Backup | Diretório de destino do `.bak` |
| Testar (permissão) | Testa escrita direta + `xp_cmdshell` |
| Deletar backup após upload | Remove `.bak` após upload bem-sucedido |
| Deletar backup em caso de falha no Upload | Remove `.bak` se upload falhar |
| Nome do Arquivo | Nome personalizado para o arquivo de backup |
| **Destino do Upload** (RadioButtons) | Mega / Drive (Conta de Serviço) / Drive (OAuth) |
| E-mail Mega / Senha Mega | Credenciais Mega (visível apenas se Mega selecionado) |
| Testar (Mega) | Testa login Mega em background, mostra resultado |
| Pasta Mega | Selecionar pasta via árvore (apenas pastas próprias) |
| ID Pasta Compartilhada | ID manual para pasta compartilhada (tem prioridade sobre nome) |
| Arquivo JSON | JSON da Service Account (visível apenas se Service Account) |
| Client ID / Client Secret | OAuth credentials (visível apenas se OAuth) |
| Autorizar | Inicia fluxo OAuth (visível apenas se OAuth) |
| Status OAuth | "Autorizado" em verde ou mensagem de erro |
| Pasta ID | ID da pasta do Google Drive |
| Data e Hora | DateTimePicker para agendamento |
| Agendar / Cancelar Agendamento | Confirma ou cancela o agendamento |
| Salvar | Persiste todas as configurações + estado do agendamento |

## Build

```
dotnet build AgendadorDeUpload\AgendadorDeUpload.csproj -c Release
```

O executável único estará em `AgendadorDeUpload\bin\Release\net48\AgendadorDeUpload.exe`.

### Dependências (mergeadas via Costura.Fody)
- `MegaApiClient` 1.10.5
- `Google.Apis.Drive.v3` 1.68.0.3568
- `Newtonsoft.Json` (transitiva do Google APIs)
- `Costura.Fody` 5.7.0

## Uso

1. Execute `AgendadorDeUpload.exe`
2. Na primeira execução: crie uma **senha mestra** (irá proteger todas as credenciais)
3. Configure servidor SQL, banco, pasta de backup, método de upload (Mega ou Google Drive)
4. Defina data/hora para o backup agendado e clique em **Agendar**
5. Clique em **Salvar** — o ícone na bandeja mostrará o status "Aguardando"
6. O backup será executado automaticamente no horário programado

### Task Scheduler

Para executar o backup diretamente (sem agendamento interno), use o argumento `/run`:

```
AgendadorDeUpload.exe /run
```

Pode ser configurado como ação no Agendador de Tarefas do Windows.

### Execução Manual

Clique com botão direito no ícone da bandeja → "Executar backup agora".