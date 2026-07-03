using System;
using Newtonsoft.Json;

namespace AgendadorDeUpload.Models
{
    [Serializable]
    public class BackupConfig
    {
        public string SqlServer { get; set; }
        public string SqlDatabase { get; set; }
        public string SqlUsername { get; set; }
        public string SqlPassword { get; set; }
        public bool UseWindowsAuth { get; set; }
        public string BackupFolder { get; set; }
        public string ServiceAccountJsonPath { get; set; }
        public string ServiceAccountJson { get; set; }
        public string GoogleDriveFolderId { get; set; }
        public string ScheduledTime { get; set; }
        public int StableSeconds { get; set; } = 30;
        public int PollIntervalMs { get; set; } = 5000;

        public string AuthMethod { get; set; } = "ServiceAccount";
        public string OAuthClientId { get; set; }
        public string OAuthClientSecret { get; set; }
        public string OAuthRefreshToken { get; set; }

        public string MegaEmail { get; set; }
        public string MegaPassword { get; set; }
        public string MegaFolder { get; set; }
        public bool DeleteAfterUpload { get; set; }
        public bool DeleteOnFailure { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static BackupConfig FromJson(string json)
        {
            return JsonConvert.DeserializeObject<BackupConfig>(json);
        }
    }
}
